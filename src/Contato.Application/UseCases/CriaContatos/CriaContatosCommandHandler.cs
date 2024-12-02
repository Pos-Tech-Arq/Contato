using Contato.Application.UseCases.CriaContatos;
using Contato.Domain.Contracts;
using Contato.Domain.ValueObjects;
using MassTransit;
using MediatR;
using Message.Contato;

namespace Contato.Application.UseCases.CriaContatos;

public class CriaContatosCommandHandler : IRequestHandler<CriaContatosCommand>
{
    private readonly IContatosRepository _contatosRepository;
    private readonly IPublishEndpoint _publisher;

    public CriaContatosCommandHandler(IContatosRepository contatosRepository, IPublishEndpoint publisher)
    {
        _contatosRepository = contatosRepository;
        _publisher = publisher;
    }

    public async Task Handle(CriaContatosCommand request, CancellationToken cT)
    {
        await Parallel.ForEachAsync(request.Contatos, new ParallelOptions { MaxDegreeOfParallelism = 10, CancellationToken = cT }, async (x, cT) =>
        {
            var telefone = new Telefone(x.Ddd, x.Numero);
            var contato = new Domain.Entities.Contato(x.Nome, x.Email, telefone);

            await _contatosRepository.Create(contato);
            await _publisher.Publish(new ContatoCriado()
            {
                Ddd = contato.Telefone.Ddd,
                MessegeId = contato.Id
            }, cT);
        });
    }
}