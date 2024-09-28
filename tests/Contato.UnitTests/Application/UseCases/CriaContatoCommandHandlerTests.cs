using Contato.Application.UseCases.CriaContato;
using Contato.Domain.Contracts;
using MassTransit;
using Message.Contato;
using Moq;

namespace Contato.UnitTests.Application.UseCases;

public class CriaContatoCommandHandlerTests
{
    private readonly Mock<IContatosRepository> _contatosRepositoryMock;
    private readonly CriaContatoCommandHandler _handler;
    private readonly Mock<IPublishEndpoint> _publisher = new();

    public CriaContatoCommandHandlerTests()
    {
        _contatosRepositoryMock = new Mock<IContatosRepository>();
        _handler = new CriaContatoCommandHandler(_contatosRepositoryMock.Object, _publisher.Object);
    }

    [Fact]
    public async Task Handle_DeveCriarContatoComSucesso()
    {
        var command = new CriaContatoCommand("11", "999999999", "Nome Teste", "email@teste.com");
        var cancellationToken = CancellationToken.None;

        _contatosRepositoryMock.Setup(r => r.Create(It.IsAny<Contato.Domain.Entities.Contato>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, cancellationToken);

        _contatosRepositoryMock.Verify(
            r => r.Create(It.Is<Contato.Domain.Entities.Contato>(contato =>
                contato.Nome == command.Nome && contato.Email == command.Email && contato.Telefone.Ddd == command.Ddd &&
                contato.Telefone.Numero == command.Numero)), Times.Once);
        _publisher.Verify(c => c.Publish(It.IsAny<ContatoCriado>(), cancellationToken), Times.Once);
    }
}