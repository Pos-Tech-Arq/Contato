using System.Text.Json;
using Contato.IntegrationTests.Factories;
using Contato.IntegrationTests.Order;

namespace Contato.IntegrationTests.Endpoints;

[TestCaseOrderer("Contato.IntegrationTests.Order.PriorityOrderer", "Contato.IntegrationTests")]
public class ContatoTests(ContatoFactory factory) : BaseIntegrationTests(factory)
{
    [Theory(DisplayName = "Busca contatos por DDD com sucesso"), TestPriority(3)]
    [InlineData("11")]
    [InlineData("21")]
    public async Task BuscaContato_DeveRetornarComSucesso_QuandoParametrosValidos(string ddd)
    {
        // Act
        var response = await Client.GetAsync($"/api/v1/contatos?ddd={ddd}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var contatos = JsonSerializer.Deserialize<List<ApiResponses.Contato>>(content);
        contatos.Should().NotBeNullOrEmpty();
        contatos.Should().HaveCount(3);
    }
}