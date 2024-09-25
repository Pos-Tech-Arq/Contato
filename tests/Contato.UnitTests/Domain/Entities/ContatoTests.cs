using AutoFixture;
using Contato.Domain.Enums;
using Contato.Domain.ValueObjects;
using FluentAssertions;

namespace Contato.UnitTests.Domain.Entities;

using Contato = Contato.Domain.Entities.Contato;

public class ContatoTests
{
    private readonly Fixture _fixture;

    public ContatoTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void Contato_DeveSerCriadoComValoresCorretos()
    {
        var nome = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = new Telefone(_fixture.Create<string>(), _fixture.Create<string>());

        var contato = new Contato(nome, email, telefone);

        contato.Nome.Should().Be(nome);
        contato.Email.Should().Be(email);
        contato.Telefone.Should().Be(telefone);
        contato.Status.Should().Be(ContatoStatus.Ativado);
        contato.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Update_DeveAtualizarValoresDoContato()
    {
        var contato = _fixture.Create<Contato>();
        var novoNome = _fixture.Create<string>();
        var novoEmail = _fixture.Create<string>();
        var novoDdd = _fixture.Create<string>();
        var novoNumero = _fixture.Create<string>();

        contato.Update(novoNome, novoEmail, novoDdd, novoNumero);

        contato.Nome.Should().Be(novoNome);
        contato.Email.Should().Be(novoEmail);
        contato.Telefone.Ddd.Should().Be(novoDdd);
        contato.Telefone.Numero.Should().Be(novoNumero);
    }

    [Fact]
    public void Contato_DeveInicializarComStatusAtivado()
    {
        var contato = _fixture.Create<Contato>();

        contato.Status.Should().Be(ContatoStatus.Ativado);
    }

    [Fact]
    public void ConstrutorPrivado_NaoDeveLancarExcecao()
    {
        var contato = (Contato)Activator.CreateInstance(typeof(Contato), true)!;

        contato.Should().NotBeNull();
    }
}