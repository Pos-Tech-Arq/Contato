using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.Infra.Configurations;

public static class ConfigureRabbitExtension
{
    public static void ConfigureRabbit(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetSnakeCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host("rabbitmq", hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
                });
                busFactoryConfigurator.UseJsonSerializer();
                busFactoryConfigurator.ConfigureEndpoints(context);
            });
        });
    }
}