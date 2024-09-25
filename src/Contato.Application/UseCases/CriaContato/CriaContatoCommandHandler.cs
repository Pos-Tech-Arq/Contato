using Contato.Application.Notifications;
using Contato.Domain.Contracts;
using Contato.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace Contato.Application.UseCases.CriaContato;

public class CriaContatoCommandHandler : IRequestHandler<CriaContatoCommand>
{
    private readonly IContatosRepository _contatosRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CriaContatoCommandHandler(IContatosRepository contatosRepository, IPublishEndpoint publishEndpoint)
    {
        _contatosRepository = contatosRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CriaContatoCommand request, CancellationToken cancellationToken)
    {
        var telefone = new Telefone(request.Ddd, request.Numero);
        var contato = new Domain.Entities.Contato(request.Nome, request.Email, telefone);

        await _contatosRepository.Create(contato);
        await _publishEndpoint.Publish(new ContatoCriado(contato), cancellationToken);
    }
}