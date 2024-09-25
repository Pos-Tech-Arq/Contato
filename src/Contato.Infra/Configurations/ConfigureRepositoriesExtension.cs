using Contato.Domain.Contracts;
using Contato.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.Infra.Configurations;

public static class ConfigureRepositoriesExtension
{
    public static void ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IContatosRepository, ContatosRepository>();
    }
}