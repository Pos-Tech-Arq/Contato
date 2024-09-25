using Contato.Domain.Enums;
using Contato.Domain.ValueObjects;

namespace Contato.Application.Notifications;

public class ContatoCriado
{
    public Guid ContatoId { get; set; }
    public Telefone Telefone { get;  set; }

    public ContatoCriado(Domain.Entities.Contato contato)
    {
        ContatoId = contato.Id;
        Telefone = contato.Telefone;
    }
}