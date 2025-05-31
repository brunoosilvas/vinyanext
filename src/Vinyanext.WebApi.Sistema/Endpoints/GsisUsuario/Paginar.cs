using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Sistema.Endpoints.GsisUsuario;

internal sealed class Paginar : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("usuario/{id:int}", async ([FromRoute] int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await Task.Delay(0, cancellationToken);

            return Results.Ok("ok");
        })
        .WithTags("Sistema / Usuário")
        .WithDescription("Teste de descrição")
        .WithSummary("anonymous")
        .WithOpenApi();
    }
}
