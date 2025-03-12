using Microsoft.AspNetCore.Authorization;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

namespace Uzerai.Dotnet.Playground.DI.Authorization.Permissions;

public class OrganizationPermissionRequiredAttribute : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
{
    public OrganizationPermissionRequiredAttribute(Permission permission)
    {
        Permission = permission;
    }

    public Permission Permission { get; }

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}