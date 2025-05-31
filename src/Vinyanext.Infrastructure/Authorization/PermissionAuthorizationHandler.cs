using Vinyanext.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Vinyanext.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler(
    IServiceScopeFactory serviceScopeFactory,
    IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        HttpContext httpContext = httpContextAccessor.HttpContext;

        if (httpContext != null &&
            httpContext.Request.Path.Value.EndsWith("openapi/v1.json", StringComparison.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
            return;
        }

        // Implementar anonymous sem autorization

        // TODO: You definitely want to reject unauthenticated users here.
        if (context.User is { Identity.IsAuthenticated: true })
        {
            // TODO: Remove this call when you implement the PermissionProvider.GetForUserIdAsync
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            PermissionProvider permissionProvider = scope.ServiceProvider.GetRequiredService<PermissionProvider>();

            int? userId = context.User.GetUserId();

            HashSet<string> permissions = await permissionProvider.GetForUserIdAsync(userId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
                return;
            }
        }

        context.Fail();
    }
}
