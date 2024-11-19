using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.Infra.Configurations;

public static class ConfigureRabbitExtension
{
    public static void ConfigureRabbit(this IServiceCollection serviceCollection, string rabbitMqHost = "rabbitmq-pod", ushort rabbitMqPort = 5672)
    {
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetSnakeCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host(rabbitMqHost, rabbitMqPort, "/", hostConfigurator =>
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