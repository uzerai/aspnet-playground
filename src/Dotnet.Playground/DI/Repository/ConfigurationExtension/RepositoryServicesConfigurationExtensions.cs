using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using Dotnet.Playground.Model.Authentication;
using Dotnet.Playground.Model.Organizations;
using Dotnet.Playground.Model.Tags;
using Route = Dotnet.Playground.Model.Route;

namespace Dotnet.Playground.DI.Repository.ConfigurationExtension;

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
        services.AddTransient<IEntityRepository<Crag>, CragRepository>();
        services.AddTransient<IEntityRepository<Pitch>, PitchRepository>();
        services.AddTransient<IEntityRepository<Note>, NoteRepository>();
        services.AddTransient<IEntityRepository<Route>, RouteRepository>();
        return services;
    }
}
