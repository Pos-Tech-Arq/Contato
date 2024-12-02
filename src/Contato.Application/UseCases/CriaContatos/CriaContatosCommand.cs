using Contato.Application.UseCases.CriaContato;
using MediatR;

namespace Contato.Application.UseCases.CriaContatos;

public class CriaContatosCommand : IRequest
{
    public CriaContatosCommand(List<CriaContatoCommand> contatos)
    {
        Contatos = contatos;
    }
    public List<CriaContatoCommand> Contatos { get; set; }
}