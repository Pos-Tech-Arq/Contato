using Contato.Infra.Contexts;
using Contato.IntegrationTests.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.IntegrationTests;

[Collection(name: nameof(ContatoFactoryCollection))]
public abstract class BaseIntegrationTests(ContatoFactory factory)
{
    private readonly AsyncServiceScope _integrationTestScope = factory.Services.CreateAsyncScope();
    protected HttpClient Client => factory.Server.CreateClient();

    protected ApplicationDbContext DbContext =>
        _integrationTestScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}