using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Data;
using Vinyanext.Application.Abstractions.Messaging;
using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Domain.Entities.Sistema;
using Vinyanext.Domain.Validations.Sistema;
using Vinyanext.Shared.Commons;
using Vinyanext.Shared.Resources;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed class LoginCommandHandler(
    IStringLocalizer<I18NResources> localizer,
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginCommand, LoginOut>
{
    public async Task<Result<LoginOut>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        GsisUsuario? usuario = await context.GsisUsuario
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Cpf == command.Login.Cpf, cancellationToken);
        
        if (usuario is null) 
        {
            return Result.Failure<LoginOut>(GsisUsuarioErros.NaoEncontrado(localizer));
        }

        var teste = passwordHasher.Hash(command.Login.Senha);

        bool saoIguais = passwordHasher.Verify(command.Login.Senha, usuario.Senha);

        if (!saoIguais)
        {
            return Result.Failure<LoginOut>(GsisUsuarioErros.NaoAutorizado(localizer));
        }

        LoginOut login = new(
            Token: tokenProvider.Create(usuario),
            RefreshToken: string.Empty
        );
        return login;
    }
}
