using MediatR;
using Vinyanext.Domain.Dtos.In.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Endpoints.Sistema.GsisUsuario;

internal sealed class Logout : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // app.MapPost("login", async (LoginIn request, ISender sender, CancellationToken cancellationToken) =>
        // {
        //     //var command = new LoginCommand(request);

        //     //Result<LoginOut> result = await sender.Send(command, cancellationToken);

        //     return Task.CompletedTask;
        // })
        // .AllowAnonymous()
        // .WithTags("Autenticação")
        // .WithSummary("/login")
        // .WithDescription("Teste de descrição")
        // .Produces<LoginOut>(StatusCodes.Status201Created)
        // .Produces(StatusCodes.Status400BadRequest)
        // .Produces(StatusCodes.Status500InternalServerError)
        // .WithOpenApi();
    }

}
