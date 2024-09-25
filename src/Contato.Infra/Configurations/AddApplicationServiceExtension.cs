using Microsoft.Extensions.DependencyInjection;

namespace Contato.Infra.Configurations;

public static class AddApplicationServiceExtension
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    }
}