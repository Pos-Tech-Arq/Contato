using Contato.Application.UseCases.RemoveContato;
using Contato.Domain.Contracts;
using MediatR;

namespace Contato.Application.UseCases.ApagaContato;

public class RemoveContatoCommandHandler : IRequestHandler<RemoveContatoCommand>
{
    private readonly IContatosRepository _contatosRepository;

    public RemoveContatoCommandHandler(IContatosRepository contatosRepository)
    {
        _contatosRepository = contatosRepository;
    }

    public async Task Handle(RemoveContatoCommand request, CancellationToken cancellationToken)
    {
        var contato = await _contatosRepository.FindById(request.Id);

        await _contatosRepository.Remove(contato);
    }
}