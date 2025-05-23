using System;
using Microsoft.AspNetCore.Authorization;

namespace Vinyanext.Infrastructure.Authorization;

internal sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}

