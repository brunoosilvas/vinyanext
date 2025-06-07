using Microsoft.Extensions.Caching.Distributed;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Database;
using Vinyanext.Application.Abstractions.Messaging;
using Vinyanext.Application.Abstractions.Services.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Shared.Commons;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed class LoginCommandHandler(
    IDistributedCache cache,
    ITokenProvider tokenProvider,
    ChangeDbContext changeDbContext,
    ILoginService loginService) : ICommandHandler<LoginCommand, LoginOut>
{
    public async Task<Result<LoginOut>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        using var context = changeDbContext(DbType.PgsqlVinyanextWrite);

        var tt = await loginService
            .Set(context)
            .Login(command.Login.Cpf, command.Login.Senha, cancellationToken);

        Guid guid = Guid.NewGuid();

        try
        {

        }
        catch (Exception) { }

        LoginOut login = new(
            Token: tokenProvider.Create(null),
            RefreshToken: guid.ToString()
        );
        return login;
    }
}
