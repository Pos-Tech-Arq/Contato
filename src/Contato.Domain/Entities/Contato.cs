using Contato.Domain.Contracts;
using Contato.Domain.Enums;
using Contato.Domain.ValueObjects;

namespace Contato.Domain.Entities;

public class Contato : Entidade, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public ContatoStatus Status { get; private set; }

    public Contato(string nome, string email, Telefone telefone)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Id = Guid.NewGuid();
        Status = ContatoStatus.Ativado;
    }

    public virtual void Update(string nome, string email, string ddd, string numero)
    {
        Nome = nome;
        Email = email;
        Telefone = new Telefone(ddd, numero);
    }

    private Contato()
    {
    }
}