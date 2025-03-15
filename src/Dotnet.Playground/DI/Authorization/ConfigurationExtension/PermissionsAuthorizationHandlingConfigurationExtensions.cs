using Microsoft.AspNetCore.Authorization;
using Dotnet.Playground.DI.Authorization.Permissions;

namespace Dotnet.Playground.DI.Authorization.ConfigurationExtension;

public static class PermissionsAuthorizationHandlingConfigurationExtensions
{
    public static IServiceCollection AddPermissionsAuthorizationHandling(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationHandler, OrganizationPermissionsAuthorizationHandler>();

        return services;
    }

}