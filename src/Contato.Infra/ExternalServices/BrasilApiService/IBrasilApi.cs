using Contato.Infra.ExternalServices.BrasilApiService.Responses;
using Refit;

namespace Contato.Infra.ExternalServices.BrasilApiService;

public interface IBrasilApi
{
    [Get("/api/ddd/v1/{ddd}")]
    Task<Region> BuscaRegiaoPorDdd(string ddd);
}