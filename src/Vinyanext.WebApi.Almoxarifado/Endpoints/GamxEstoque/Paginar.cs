using System;
using MediatR;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Almoxarifado.Endpoints.GamxEstoque;

public sealed class Paginar : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("almoxarifado/estoque", async (ISender sender, CancellationToken cancellationToken) =>
        {
            await Task.Delay(0, cancellationToken);

            return Results.Ok("ok");
        })
        .RequireAuthorization("almoxarifado")
        .WithDescription("Teste de descrição")
        .WithTags("Sistema / Usuário")
        .WithOpenApi();
    }
}
