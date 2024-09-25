using Contato.Application.UseCases.CriaContato;
using Contato.Domain.Contracts;
using Moq;

namespace Contato.UnitTests.Application.UseCases;

public class CriaContatoCommandHandlerTests
{
    private readonly Mock<IContatosRepository> _contatosRepositoryMock;
    private readonly CriaContatoCommandHandler _handler;

    public CriaContatoCommandHandlerTests()
    {
        _contatosRepositoryMock = new Mock<IContatosRepository>();
        _handler = new CriaContatoCommandHandler(_contatosRepositoryMock.Object);
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
    }
}