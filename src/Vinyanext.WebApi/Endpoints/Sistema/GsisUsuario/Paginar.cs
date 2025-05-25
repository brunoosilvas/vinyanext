using System;
using MediatR;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Endpoints.Sistema.GsisUsuario;

public sealed class Paginar : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/login", async (ISender sender, CancellationToken cancellationToken) =>
        {
            await Task.Delay(0, cancellationToken);

            return Results.Ok("ok");
        })
        .RequireAuthorization()
        .WithDescription("Teste de descrição")
        .WithTags("Sistema / Usuário")
        .WithOpenApi();
    }
}
