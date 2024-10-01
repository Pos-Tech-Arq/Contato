using System.Reflection;
using Contato.Application.UseCases.ApagaContato;
using Contato.Application.UseCases.AtualizaContato;
using Contato.Application.UseCases.CriaContato;
using Contato.Application.UseCases.RemoveContato;
using Contato.Domain.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.Infra.Configurations;

public static class AddApplicationServiceExtension
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRequestHandler<AtualizaContatoCommand>, AtualizaContatoCommandHandler>();
        serviceCollection.AddScoped<IRequestHandler<CriaContatoCommand>, CriaContatoCommandHandler>();
        serviceCollection.AddScoped<IRequestHandler<RemoveContatoCommand>, RemoveContatoCommandHandler>();
        serviceCollection.AddMediatR(c => c.RegisterServicesFromAssemblies(Assembly.Load("Contato.Application")));
    }
}