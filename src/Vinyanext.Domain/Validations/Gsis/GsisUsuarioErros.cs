using Microsoft.Extensions.Localization;
using Vinyanext.Shared.Commons;
using Vinyanext.Shared.Resources;

namespace Vinyanext.Domain.Validations.Gsis;

public static class UsuarioErros
{
    public static Error NaoEncontrado(IStringLocalizer<I18NResources> localizer) => Error.NotFound(
        "Gsis.NaoEncontrato",
        localizer["GsisUsuario.NaoEncontrado"]
    );

    public static Error NaoAutorizado(IStringLocalizer<I18NResources> localizer) => Error.Validation(
        "Gsis.NaoAutorizado",
        localizer["GsisUsuario.NaoAutorizado"]
    );
}
