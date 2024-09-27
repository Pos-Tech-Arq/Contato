using Contato.Domain.Contracts;
using Contato.Domain.ValueObjects;
using MassTransit;
using MediatR;
using Message.Contato;

namespace Contato.Application.UseCases.CriaContato;

public class CriaContatoCommandHandler : IRequestHandler<CriaContatoCommand>
{
    private readonly IContatosRepository _contatosRepository;
    private readonly IPublishEndpoint _publisher;

    public CriaContatoCommandHandler(IContatosRepository contatosRepository, IPublishEndpoint publisher)
    {
        _contatosRepository = contatosRepository;
        _publisher = publisher;
    }

    public async Task Handle(CriaContatoCommand request, CancellationToken cancellationToken)
    {
        var telefone = new Telefone(request.Ddd, request.Numero);
        var contato = new Domain.Entities.Contato(request.Nome, request.Email, telefone);

        await _contatosRepository.Create(contato);
        await _publisher.Publish(new ContatoCriado()
        {
            Ddd = contato.Telefone.Ddd,
            MessegeId = contato.Id
        }, cancellationToken);
    }
}