using Route = Dotnet.Playground.Model.Route;

namespace Dotnet.Playground.DI.Repository.Interface;

public interface IRouteRepository : IEntityRepository<Route>
{
    // Add any Route-specific repository methods here
} 