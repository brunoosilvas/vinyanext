using System;
using Microsoft.EntityFrameworkCore;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Data;
using Vinyanext.Application.Abstractions.Messaging;
using Vinyanext.Domain.Entities.Sistema;
using Vinyanext.Shared.Commons;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed class LoginCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginCommand, string>
{
    public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        GsisUsuario? usuario = await context.GsisUsuario
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Usuario == command.Usuario, cancellationToken);
        
        if (usuario is null) 
        {
            return Result.Failure<string>(null);
        }

        string token = tokenProvider.Create(usuario);
        return token;
    }
}
