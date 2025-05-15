using System;

namespace Vinyanext.WebApi.Sistema.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

