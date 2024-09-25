using Contato.Domain.Entities;
using Contato.Infra.Contexts.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Contato.Infra.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Domain.Entities.Contato> Contatos { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ContatoMapping().Configure(modelBuilder.Entity<Domain.Entities.Contato>());
    }
}