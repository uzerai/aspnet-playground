using Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Uzerai.Dotnet.Playground.DI.Repository.ConfigurationExtension;

public static class RepositoryServicesConfigurationExtensions
{
    /// <summary>
    /// Registers project repository services.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        services.AddTransient<IOrganizationUserRepository, OrganizationUserRepository>();
        services.AddTransient<IOrganizationTeamRepository, OrganizationTeamRepository>();
        services.AddTransient<ITagRepository, TagRepository>();

        return services;
    }
}
