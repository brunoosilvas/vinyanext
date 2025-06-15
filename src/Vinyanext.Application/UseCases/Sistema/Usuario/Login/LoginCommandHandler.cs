using Microsoft.Extensions.Caching.Distributed;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Databases;
using Vinyanext.Application.Abstractions.Messaging;
using Vinyanext.Application.Abstractions.Services.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Shared.Commons;
using Vinyanext.Shared.Constants;

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

        LoginOut login = null;

        if (string.Equals(Authentications.Token, command.Login.Tipo, StringComparison.OrdinalIgnoreCase))
        {
            var resultado = await loginService
                .Set(context)
                .Login(command.Login.Cpf, command.Login.Senha, cancellationToken);

            if (resultado.IsFailure)
            {
                return Result.Failure<LoginOut>(resultado.Error);
            }



            // login = new(
            //     Token: tokenProvider.Create(resultado.Value),
            //     RefreshToken: guid.ToString()
            // );

        }
        else
        {


        }

        return login;
    }
}
