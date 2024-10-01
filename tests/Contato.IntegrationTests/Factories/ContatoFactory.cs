using Contato.Infra.Contexts;
using Contato.IntegrationTests.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.IntegrationTests.Factories;

[CollectionDefinition(nameof(ContatoFactoryCollection))]
public class ContatoFactoryCollection : ICollectionFixture<ContatoFactory>;

public class ContatoFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly DockerFixture DockerFixture = new();

    public async Task InitializeAsync()
    {
        await DockerFixture.InitializeAsync();
        ExecuteScript("create_table_contatos.sql");
        ExecuteScript("insert_into_contatos_table.sql");
    }

    public new async Task DisposeAsync() => await DockerFixture.DisposeAsync();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<ApplicationDbContext>);
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(GetConnectionString(), x => x.MigrationsAssembly("Contato.Infra"));
            });
            
            // services.AddMediatR(cfg =>
            // {
            //     cfg.RegisterServicesFromAssemblyContaining<YourHandlerType>(); // Only scan relevant assembly
            // });
        });
    }

    public string GetConnectionString()
    {
        return
            $"Server=localhost,{DockerFixture.MsSqlContainer.GetMappedPublicPort(1433)};User=sa;Password=Strong_password_123!;TrustServerCertificate=True";
    }

    public void ExecuteScript(string scriptName)
    {
        var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "Sql", scriptName);
        string query = File.ReadAllText(scriptPath);

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            connection.Open();
            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}