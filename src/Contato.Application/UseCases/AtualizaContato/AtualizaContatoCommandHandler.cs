using Contato.Domain.Commands;
using Contato.Domain.Contracts;
using MediatR;

namespace Contato.Application.UseCases.AtualizaContato;

public class AtualizaContatoCommandHandler : IRequestHandler<AtualizaContatoCommand>
{
    private readonly IContatosRepository _contatosRepository;

    public AtualizaContatoCommandHandler(IContatosRepository contatosRepository)
    {
        _contatosRepository = contatosRepository;
    }

    public async Task Handle(AtualizaContatoCommand request, CancellationToken cancellationToken)
    {
        var contato = await _contatosRepository.FindById(request.Id);
        contato.Update(request.Nome, request.Email, request.Ddd, request.Numero);

        await _contatosRepository.Update(contato);
    }
}