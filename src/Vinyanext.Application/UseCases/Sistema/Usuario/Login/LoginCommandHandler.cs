using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Data;
using Vinyanext.Application.Abstractions.Messaging;
using Vinyanext.Domain.Entities.Sistema;
using Vinyanext.Domain.Validations.Sistema;
using Vinyanext.Shared.Commons;
using Vinyanext.Shared.Resources;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed class LoginCommandHandler(
    IStringLocalizer<I18NResources> localizer,
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginCommand, string>
{
    public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        GsisUsuario? usuario = await context.GsisUsuario
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Cpf == command.Cpf, cancellationToken);
        
        if (usuario is null) 
        {
            return Result.Failure<string>(GsisUsuarioErros.NaoEncontrado(localizer));
        }

        var teste = passwordHasher.Hash(command.Senha);

        bool saoIguais = passwordHasher.Verify(command.Senha, usuario.Senha);

        if (!saoIguais)
        {
            return Result.Failure<string>(GsisUsuarioErros.NaoAutorizado(localizer));
        }

        string token = tokenProvider.Create(usuario);
        return token;
    }
}
