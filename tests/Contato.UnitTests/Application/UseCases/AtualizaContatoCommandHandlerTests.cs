using Contato.Application.UseCases.AtualizaContato;
using Contato.Domain.Commands;
using Contato.Domain.Contracts;
using FluentAssertions;
using Moq;

namespace Contato.UnitTests.Application.UseCases;

public class AtualizaContatoCommandHandlerTests
{
    private readonly Mock<IContatosRepository> _contatosRepositoryMock;
    private readonly AtualizaContatoCommandHandler _handler;

    public AtualizaContatoCommandHandlerTests()
    {
        _contatosRepositoryMock = new Mock<IContatosRepository>();
        _handler = new AtualizaContatoCommandHandler(_contatosRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_DeveAtualizarContatoComSucesso()
    {
        var contatoId = Guid.NewGuid();
        var command = new AtualizaContatoCommand(contatoId, "11", "999999999", "Novo Nome", "novoemail@teste.com");
        var contatoMock = new Mock<Contato.Domain.Entities.Contato>("Nome Antigo", "email@teste.com",
            new Contato.Domain.ValueObjects.Telefone("11", "123456789"));

        _contatosRepositoryMock.Setup(r => r.FindById(contatoId))
            .ReturnsAsync(contatoMock.Object);

        _contatosRepositoryMock.Setup(r => r.Update(contatoMock.Object))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        contatoMock.Verify(x => x.Update(command.Nome, command.Email, command.Ddd, command.Numero), Times.Once);
        _contatosRepositoryMock.Verify(r => r.Update(contatoMock.Object), Times.Once);
    }
}