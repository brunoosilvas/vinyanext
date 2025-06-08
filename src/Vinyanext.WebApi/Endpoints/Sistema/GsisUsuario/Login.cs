using MediatR;
using Vinyanext.Application.UseCases.Sistema.Usuario.Login;
using Vinyanext.Infrastructure.Abstractions.Endpoints;
using Vinyanext.Shared.Extensions;
using Vinyanext.Shared.Commons;
using Vinyanext.WebApi.Commons;
using Vinyanext.Domain.Dtos.In.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;

namespace Vinyanext.WebApi.Endpoints.Sistema.GsisUsuario;

internal sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async (LoginIn request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new LoginCommand(request);

            Result<LoginOut> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        })
        .AllowAnonymous()
        .WithTags("Autenticação")
        .WithSummary("/login")
        .WithDescription("Teste de descrição")
        .Produces<LoginOut>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi();
    }
}
