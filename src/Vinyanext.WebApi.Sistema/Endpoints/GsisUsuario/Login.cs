using MediatR;
using Vinyanext.Infrastructure.Abstractions.Endpoints;

namespace Vinyanext.WebApi.Sistema.Endpoints.GsisUsuario;

internal sealed class Login : IEndpoint
{
    public sealed record Request(string Email, string Password);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login-teste", async (Request request, ISender sender, CancellationToken cancellationToken) => 
        {
            await Task.Delay(0, cancellationToken);
            
            return Task.CompletedTask;
        })
        //.WithGroupName("Sistema / Usuário")
        //.WithSummary("Teste de sumario")
        .WithDescription("Teste de descrição")                
        .WithTags("Sistema / Usuário")
        .WithOpenApi();
    }
}
