using MediatR;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Sistema.Endpoints.GsisUsuario;

internal sealed class Paginar : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("usuario", async (ISender sender, CancellationToken cancellationToken) =>
        {
            await Task.Delay(0, cancellationToken);

            return Results.Ok("ok");
        })
        //.RequireAuthorization("sistema")
        .AllowAnonymous()
        .WithDescription("Teste de descrição")
        .WithTags("Sistema / Usuário")
        .WithOpenApi();
    }
}
