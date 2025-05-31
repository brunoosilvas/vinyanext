using System;
using MediatR;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Endpoints.Sistema.GsisUsuario;

public sealed class Paginar : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("autenticado", async (ISender sender, CancellationToken cancellationToken) =>
        {
            await Task.Delay(0, cancellationToken);

            return Results.Ok("ok");
        })
        .WithTags("Sistema / Usuário")
        .WithDescription("Teste de descrição")
        .WithSummary("default, sistema")
        .WithOpenApi();
    }
}
