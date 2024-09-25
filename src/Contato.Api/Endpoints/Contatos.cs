using Contato.Api.Requests;
using Contato.Application.UseCases.ApagaContato;
using Contato.Application.UseCases.CriaContato;
using Contato.Application.UseCases.RemoveContato;
using Contato.Domain.Commands;
using Contato.Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contato.Api.Endpoints;

public static class Contatos
{
    public static void RegisterContatosEndpoints(this IEndpointRouteBuilder routes)
    {
        var contatoRoute = routes.MapGroup("/api/v1/contatos");

        contatoRoute.MapPost("", async (IMediator mediator, [FromBody] CriaContatoRequest request) =>
            {
                await mediator.Send(new CriaContatoCommand(request.Telefone.Ddd, request.Telefone.Numero, request.Nome,
                    request.Email));

                return TypedResults.NoContent();
            })
            .WithName("CriaContato")
            .WithOpenApi()
            .AddFluentValidationFilter();

        contatoRoute.MapGet("", async (IContatosRepository contatosRepository, [FromQuery] string? ddd) =>
            {
                var contatos = await contatosRepository.SearchRegiao(ddd);

                return TypedResults.Ok(contatos);
            })
            .WithName("BuscaContatos")
            .WithOpenApi()
            .AddFluentValidationFilter();

        contatoRoute.MapPut("{id}",
                async (IMediator mediator, [FromBody] AtualizaContatoRequest request, [FromRoute] Guid id) =>
                {
                    await mediator.Send(new AtualizaContatoCommand(id, request.Telefone.Ddd, request.Telefone.Numero,
                        request.Nome, request.Email));

                    return TypedResults.NoContent();
                })
            .WithName("AtualizaContato")
            .WithOpenApi()
            .AddFluentValidationFilter();

        contatoRoute.MapDelete("{id}", async (IMediator removeContatoService, [FromRoute] Guid id) =>
            {
                await removeContatoService.Send(new RemoveContatoCommand(id));

                return TypedResults.NoContent();
            })
            .WithName("RemoveContato")
            .WithOpenApi()
            .AddFluentValidationFilter();
    }
}