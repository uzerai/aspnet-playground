using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Playground.DI.Authorization.Permissions;

public class PlatformPermissionsAuthorizationHandler : AuthorizationHandler<PlatformPermissionRequiredAttribute>
{
    private readonly ILogger<PlatformPermissionsAuthorizationHandler> _logger;

    public PlatformPermissionsAuthorizationHandler(ILogger<PlatformPermissionsAuthorizationHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PlatformPermissionRequiredAttribute requirement)
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

        // This is the local user context that the LocalUserContextMiddleware sets.
        // It should always be there since all of this code is post-authentication.
        // 
        // This is fluff to make the compiler happy.
        var localUser = httpContext.GetLocalUser();
        if (localUser == null)
        {
            _logger.LogError("LocalUser from HttpContext resource is null; aborting authorization check.");
            context.Fail();
            return Task.CompletedTask;
        }

        var platformPermissions = localUser.PlatformPermissions.ToList();

        if (platformPermissions.Count < 1)
        {
            _logger.LogError("User has no platform permissions; aborting authorization check.");
            context.Fail();
            return Task.CompletedTask;
        }

        if (!platformPermissions.Any(perm => perm == requirement.Permission))
        {
            context.Fail();
        }
        else
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}