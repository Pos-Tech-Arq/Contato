using Contato.Infra.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.IntegrationTests.Infra;

[Collection(name: nameof(ContatosFactoryCollection))]
public abstract class IntegrationTests(ContatoFactory factory)
{
    private readonly AsyncServiceScope _integrationTestScope = factory.Services.CreateAsyncScope();
    protected HttpClient Client => factory.Server.CreateClient();

    protected ApplicationDbContext DbContext =>
        _integrationTestScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}