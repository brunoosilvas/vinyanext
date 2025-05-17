using Microsoft.Extensions.Localization;
using Vinyanext.Shared.Commons;
using Vinyanext.Shared.Resources;

namespace Vinyanext.Domain.Validations.Sistema;

public class GsisUsuarioErros
{
    public static Error NaoEncontrado(IStringLocalizer<I18NResources> localizer) => Error.NotFound(
        "GsisUsuario.NaoEncontrato",
        localizer["GsisUsuario.NaoEncontrado"]
    );

    public static Error NaoAutorizado(IStringLocalizer<I18NResources> localizer) => Error.Validation(
        "GsisUsuario.NaoAutorizado",
        localizer["GsisUsuario.NaoAutorizado"]
    );
}
