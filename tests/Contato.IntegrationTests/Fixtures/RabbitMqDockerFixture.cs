using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Contato.IntegrationTests.Fixtures;

public class RabbitMqDockerFixture
{
    private readonly IContainer _rabbitMqContainer = new ContainerBuilder()
        .WithImage("rabbitmq:management")
        .WithPortBinding(15672, 15672)
        .WithPortBinding(5672, 5672)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
        .WithAutoRemove(true)
        .WithCleanUp(true)
        .Build();

    public Task InitializeAsync()
    {
        return _rabbitMqContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _rabbitMqContainer.DisposeAsync().AsTask();
    }
}