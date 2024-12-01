using Contato.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.Infra.Configurations;

public static class ConfigureDatabaseExtension
{
    public static void ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("DefaultConnection");
        if (cs.Contains("@server"))
            cs = cs.Replace("@server", Environment.GetEnvironmentVariable("Sql_Server"));
        serviceCollection.AddDbContext<ApplicationDbContext>(
            options =>
                options.UseSqlServer(cs,
                    x => x.MigrationsAssembly("Contato.Infra")
                ));
    }
}