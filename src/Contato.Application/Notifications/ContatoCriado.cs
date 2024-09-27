using Contato.Domain.Enums;
using Contato.Domain.ValueObjects;
using MassTransit;

namespace Message.Contato
{
    [EntityName("contato_criado")]
    public class ContatoCriado
    {
        public Guid MessegeId { get; set; }
        public string Ddd { get; set; }
    }
}