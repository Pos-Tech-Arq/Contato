using Contato.Application.UseCases.RemoveContato;

namespace Contato.UnitTests.Application.UseCases;

using System.Threading;
using System.Threading.Tasks;
using Contato.Application.UseCases.ApagaContato;
using Contato.Domain.Contracts;
using Moq;
using Xunit;
using FluentAssertions;

public class RemoveContatoCommandHandlerTests
{
    private readonly Mock<IContatosRepository> _contatosRepositoryMock;
    private readonly RemoveContatoCommandHandler _handler;

    public RemoveContatoCommandHandlerTests()
    {
        _contatosRepositoryMock = new Mock<IContatosRepository>();
        _handler = new RemoveContatoCommandHandler(_contatosRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_DeveRemoverContatoComSucesso()
    {
        var contatoId = Guid.NewGuid();
        var command = new RemoveContatoCommand(contatoId);
        var contatoMock = new Mock<Contato.Domain.Entities.Contato>(null, null, null);

        _contatosRepositoryMock.Setup(r => r.FindById(contatoId))
            .ReturnsAsync(contatoMock.Object);

        _contatosRepositoryMock.Setup(r => r.Remove(It.IsAny<Contato.Domain.Entities.Contato>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _contatosRepositoryMock.Verify(r => r.FindById(contatoId), Times.Once);
        _contatosRepositoryMock.Verify(r => r.Remove(contatoMock.Object), Times.Once);
        contatoMock.Object.Should().NotBeNull();
    }
}
