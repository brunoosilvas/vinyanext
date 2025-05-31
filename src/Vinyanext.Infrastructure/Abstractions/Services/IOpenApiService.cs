using System;

namespace Vinyanext.Infrastructure.Abstractions.Services;

public interface IOpenApiService
{
    Task MapMetadataEndpointAsync(string id);
}
