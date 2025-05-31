using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Shared.Exceptions;
using Vinyanext.Shared.Resources;

namespace Vinyanext.Infrastructure.Authentication;

internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor,
    IStringLocalizer<I18NResources> localizer
    ) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IStringLocalizer<I18NResources> _localizer = localizer;

    // public Guid UserId =>
    //     _httpContextAccessor
    //         .HttpContext?
    //         .User
    //         .GetUserId() ??
    //     throw new AppException(_localizer["App.Usuario.ContextoNaoDisponivel"]);

    public int? UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new AppException(_localizer["App.Usuario.ContextoNaoDisponivel"]);
}
