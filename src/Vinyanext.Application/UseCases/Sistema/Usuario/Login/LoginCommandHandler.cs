using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Database;
using Vinyanext.Application.Abstractions.Messaging;
using Vinyanext.Application.Abstractions.Services.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Domain.Entities.Sistema;
using Vinyanext.Domain.Validations.Sistema;
using Vinyanext.Shared.Commons;
using Vinyanext.Shared.Resources;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed class LoginCommandHandler(
    IDistributedCache cache,
    IStringLocalizer<I18NResources> localizer,
    IPasswordProvider passwordHasher,
    ITokenProvider tokenProvider,
    ChangeDbContext changeDbContext,
    ILoginService loginService) : ICommandHandler<LoginCommand, LoginOut>
{
    public async Task<Result<LoginOut>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        using var context = changeDbContext(DbType.PgsqlVinyanextWrite);

        var tt = await loginService
            .Set<ILoginService>(context)
            .Login("", "", cancellationToken);


        GsisUsuario? usuario = await context.GsisUsuario
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Cpf == command.Login.Cpf, cancellationToken);

        if (usuario is null)
        {
            return Result.Failure<LoginOut>(GsisUsuarioErros.NaoEncontrado(localizer));
        }

        bool saoIguais = passwordHasher.Verify(command.Login.Senha, usuario.Senha);

        if (!saoIguais)
        {
            return Result.Failure<LoginOut>(GsisUsuarioErros.NaoAutorizado(localizer));
        }

        Guid guid = Guid.NewGuid();

        try
        {

        }
        catch (Exception) { }

        LoginOut login = new(
            Token: tokenProvider.Create(usuario),
            RefreshToken: guid.ToString()
        );
        return login;
    }
}
