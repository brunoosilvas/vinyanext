using System.Security.Claims;
using Vinyanext.Shared.Exceptions;

namespace Vinyanext.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(userId, out int parseUserId) ?
            parseUserId :
            throw new AppException("User id is unavailable");
    }
}

