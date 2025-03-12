using Microsoft.AspNetCore.Authorization;

namespace Uzerai.Dotnet.Playground.DI.Authorization.Permissions;

public class OrganizationPermissionsAuthorizationHandler : AuthorizationHandler<OrganizationPermissionRequiredAttribute>
{
    private readonly ILogger<OrganizationPermissionsAuthorizationHandler> _logger;

    public OrganizationPermissionsAuthorizationHandler(ILogger<OrganizationPermissionsAuthorizationHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OrganizationPermissionRequiredAttribute requirement)
    {
        _logger.LogDebug("Handling organization permissions authorization for {Permission}", requirement.Permission);

        // This is a bit of a hack but, let's use the same local user context that the LocalUserContextMiddleware sets.
        // This way we don't have to query again for the user and permissions.
        var httpContext = context.Resource as HttpContext;
        if (httpContext == null)
        {
            _logger.LogError("HttpContext is null; aborting authorization check.");
            context.Fail();
            return Task.CompletedTask;
        }

        var organizationIdFromRoute = httpContext.GetRouteValue("organizationId") as string;
        if (organizationIdFromRoute == null)
        {
            _logger.LogError("OrganizationPermissionRequiredAttribute assigned on organization-less endpoint; aborting authorization check.");
            context.Fail();
            return Task.CompletedTask;
        }
        var organizationIdToAuthorizeAgainst = Guid.Parse(organizationIdFromRoute);
        
        var localUser = httpContext.GetLocalUser();
        if (localUser == null)
        {
            _logger.LogError("LocalUser from HttpContext resource is null; aborting authorization check.");
            context.Fail();
            return Task.CompletedTask;
        }

        var organizationPermissions = localUser.OrganizationUsers
            .Where(ou => ou.OrganizationId == organizationIdToAuthorizeAgainst)
            .SelectMany(ou => ou.Permissions)
            .ToList();
        
        if (organizationPermissions == null)
        {
            _logger.LogError("User has no permissions for organization {OrganizationId}; aborting authorization check.", organizationIdToAuthorizeAgainst);
            context.Fail();
            return Task.CompletedTask;
        }

        if(!organizationPermissions.Any(orgPerm => orgPerm.Permission == requirement.Permission))
        {
            context.Fail();
        } else {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}