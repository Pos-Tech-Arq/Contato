using MediatR;

namespace Contato.Application.UseCases.RemoveContato;

public class RemoveContatoCommand : IRequest
{
    public Guid Id { get; set; }

    public RemoveContatoCommand(Guid id)
    {
        Id = id;
    }
}