using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Vinyanext.Infrastructure.Abstractions.Services;

namespace Vinyanext.Infrastructure.Services;

public sealed class OpenApiService(
    IConfiguration configuration,
    IHttpClientFactory httpClientFactory,
    IDistributedCache cache
    ) : IOpenApiService
{
    public async Task MapMetadataEndpointAsync(string id)
    {
        await Task.Delay(0);

        using var httpClient = httpClientFactory.CreateClient();

        var teste = configuration.GetSection($"OpenApi:{id}").Get<string>();
        var tt = teste.AsEnumerable();
    }
}
