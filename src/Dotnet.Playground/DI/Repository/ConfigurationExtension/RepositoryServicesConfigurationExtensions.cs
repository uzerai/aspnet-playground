using Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Organizations;
using Uzerai.Dotnet.Playground.Model.Tags;

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
        services.AddTransient<IEntityRepository<User>, UserRepository>();
        services.AddTransient<IEntityRepository<Organization>, OrganizationRepository>();
        services.AddTransient<IRepository<OrganizationUser>, OrganizationUserRepository>();
        services.AddTransient<IEntityRepository<OrganizationTeam>, OrganizationTeamRepository>();
        services.AddTransient<IEntityRepository<Tag>, TagRepository>();

        return services;
    }
}
