using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using NodaTime;
using Route = Dotnet.Playground.Model.Location.Route;

namespace Dotnet.Playground.DI.Repository;

public class RouteRepository : EntityRepository<Route>, IRouteRepository
{
    public RouteRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }
} 