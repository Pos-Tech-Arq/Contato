namespace Contato.Domain.Contracts;

using Contato = Contato.Domain.Entities.Contato;

public interface IContatosRepository
{
    Task Create(Contato contato);

    Task Update(Contato contato);

    Task<Contato> FindById(Guid id);

    Task Remove(Contato contato);

    Task<IEnumerable<Contato>> SearchRegiao(string? ddd);
}