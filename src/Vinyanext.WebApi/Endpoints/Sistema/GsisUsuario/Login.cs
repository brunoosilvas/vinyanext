
using MediatR;
using Vinyanext.Application.UseCases.Sistema.Usuario.Login;
using Vinyanext.Infrastructure.Abstractions.Endpoints;
using Vinyanext.Shared.Extensions;
using Vinyanext.Shared.Commons;
using Vinyanext.WebApi.Commons;

namespace Vinyanext.WebApi.Endpoints.Sistema.GsisUsuario;

internal sealed class Login : IEndpoint
{
    public sealed record LoginIn(string Cpf, string Senha);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async (LoginIn request, ISender sender, CancellationToken cancellationToken) => 
        {
            var command = new LoginCommand(request.Cpf, request.Senha);
            
            Result<string> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }) 
        .WithDescription("Teste de descrição")                
        .WithTags("Sistema / Usuário")
        .WithOpenApi();
    }
}
