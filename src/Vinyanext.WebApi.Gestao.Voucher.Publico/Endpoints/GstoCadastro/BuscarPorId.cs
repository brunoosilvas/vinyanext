using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Gestao.Voucher.Publico.Endpoints.GstoCadastro;

internal sealed class BuscarPorId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("usuario/{id:int}", async ([FromRoute] int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await Task.Delay(0, cancellationToken);

            return Results.Ok("ok");
        })
        .WithTags("Gestão Voucher / Usuário")
        .WithDescription("Teste de descrição")
        .WithSummary("anonymous")
        .WithOpenApi();
    }
}
