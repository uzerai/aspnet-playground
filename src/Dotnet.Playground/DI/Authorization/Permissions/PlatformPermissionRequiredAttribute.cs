using Microsoft.AspNetCore.Authorization;
using Dotnet.Playground.Model.Authorization.Permissions;

namespace Dotnet.Playground.DI.Authorization.Permissions;

public class PlatformPermissionRequiredAttribute : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
{
    public PlatformPermissionRequiredAttribute(Permission permission)
    {
        Permission = permission;
    }

    public Permission Permission { get; }

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}