using Uzerai.Dotnet.Playground.DI.Repository.Interface;

namespace Uzerai.Dotnet.Playground.DI.Repository.ConfigurationExtension;

public static class RepositoryServicesConfigurationExtensions
{
    /// <summary>
    /// Registers project repository services.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<OrganizationRepository>();
        services.AddScoped<OrganizationUserRepository>();
        services.AddScoped<OrganizationTeamRepository>();
        services.AddScoped<TagRepository>();

        return services;
    }
}
