using System.Text.Json.Serialization;

namespace Contato.Infra.ExternalServices.BrasilApiService.Responses;

public class Region
{
    
    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("cities")]
    public List<string> cities { get; set; }
}