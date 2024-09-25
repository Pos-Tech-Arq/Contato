using MediatR;

namespace Contato.Domain.Commands;

public class AtualizaContatoCommand : IRequest
{
    public Guid Id { get; set; }
    public string Ddd { get; set; }
    public string Numero { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public AtualizaContatoCommand(Guid id, string ddd, string numero, string nome, string email)
    {
        Ddd = ddd;
        Numero = numero;
        Nome = nome;
        Email = email;
        Id = id;
    }
}