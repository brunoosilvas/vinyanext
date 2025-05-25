using Microsoft.AspNetCore.Http;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Shared.Errors;

namespace Vinyanext.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new AppException("User context is unavailable");
}
