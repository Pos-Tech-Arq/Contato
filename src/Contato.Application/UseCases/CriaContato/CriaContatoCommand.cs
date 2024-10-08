﻿using MediatR;

namespace Contato.Application.UseCases.CriaContato;

public class CriaContatoCommand : IRequest
{
    public string Ddd { get; set; }
    public string Numero { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public CriaContatoCommand(string ddd, string numero, string nome, string email)
    {
        Ddd = ddd;
        Numero = numero;
        Nome = nome;
        Email = email;
    }
}