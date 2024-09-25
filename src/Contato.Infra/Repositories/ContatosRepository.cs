using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Contato.Domain.Contracts;
using Contato.Infra.Contexts;

namespace Contato.Infra.Repositories;

using Contato = Domain.Entities.Contato;

public class ContatosRepository : IContatosRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Contato> _dbSet;

    public ContatosRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Contatos;
    }

    public async Task Create(Contato contato)
    {
        await _dbSet.AddAsync(contato);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task Update(Contato contato)
    {
        _dbSet.Update(contato);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Contato>> SearchRegiao(string? ddd)
    {
        var query = _dbSet.AsQueryable();

        if (!ddd.IsNullOrEmpty())
        {
            query = query.Where(c => c.Telefone.Ddd == ddd);
        }

        return await query.ToListAsync();
    }

    public async Task<Contato> FindById(Guid id)
    {
        return await _dbSet.FirstAsync(c => c.Id == id);
    }

    public async Task Remove(Contato contato)
    {
        _dbSet.Remove(contato);
        await _applicationDbContext.SaveChangesAsync();
    }
}