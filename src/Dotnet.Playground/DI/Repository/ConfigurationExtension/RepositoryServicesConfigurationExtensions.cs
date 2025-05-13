using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using Route = Dotnet.Playground.Model.Location.Route;

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
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        services.AddTransient<IOrganizationUserRepository, OrganizationUserRepository>();
        services.AddTransient<IOrganizationTeamRepository, OrganizationTeamRepository>();
        services.AddTransient<IAreaRepository, AreaRepository>();
        services.AddTransient<ISectorRepository, SectorRepository>();
        services.AddTransient<IPitchRepository, PitchRepository>();
        services.AddTransient<IEntityRepository<Route>, RouteRepository>();
        services.AddTransient<IEntityRepository<Image>, ImageRepository>();
        
        return services;
    }
}
