using Contato.Domain.Contracts;
using Contato.Domain.ValueObjects;
using MediatR;

namespace Contato.Application.UseCases.CriaContato;

public class CriaContatoCommandHandler : IRequestHandler<CriaContatoCommand>
{
    private readonly IContatosRepository _contatosRepository;

    public CriaContatoCommandHandler(IContatosRepository contatosRepository)
    {
        _contatosRepository = contatosRepository;
    }

    public async Task Handle(CriaContatoCommand request, CancellationToken cancellationToken)
    {
        var telefone = new Telefone(request.Ddd, request.Numero);
        var contato = new Domain.Entities.Contato(request.Nome, request.Email, telefone);

        await _contatosRepository.Create(contato);
    }
}