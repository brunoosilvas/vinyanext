using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Database;
using Vinyanext.Application.Abstractions.Services.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Domain.Entities.Sistema;
using Vinyanext.Domain.Validations.Sistema;
using Vinyanext.Shared.Commons;
using Vinyanext.Shared.Resources;

namespace Vinyanext.Application.Services.Sistema;

public class LoginService(
    IPasswordProvider passwordProvider,
    IStringLocalizer<I18NResources> localizer
    ) : ILoginService
{
    public IApplicationDbContextBase Context { get; set; } = null!;

    public async Task<Result<LoginOut>> Login(string cpf, string senha, CancellationToken cancellationToken)
    {
        GsisUsuario? usuario = await Context.GsisUsuario
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Cpf == cpf, cancellationToken);

        if (usuario is null)
        {
            return Result.Failure<LoginOut>(GsisUsuarioErros.NaoEncontrado(localizer));
        }

        bool saoIguais = passwordProvider.Verify(senha, usuario.Senha);

        if (!saoIguais)
        {
            return Result.Failure<LoginOut>(GsisUsuarioErros.NaoAutorizado(localizer));
        }

        return null;
    }
}
