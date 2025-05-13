using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Location;
using NodaTime;

namespace Dotnet.Playground.DI.Repository;

public class AreaRepository : EntityRepository<Area>, IAreaRepository
{
    public AreaRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }
} 