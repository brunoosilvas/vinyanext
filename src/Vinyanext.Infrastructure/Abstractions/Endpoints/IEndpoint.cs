using Microsoft.AspNetCore.Routing;

namespace Vinyanext.Infrastructure.Abstractions.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
