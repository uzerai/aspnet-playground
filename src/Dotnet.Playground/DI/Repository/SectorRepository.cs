using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using NodaTime;

namespace Dotnet.Playground.DI.Repository;

public class SectorRepository : EntityRepository<Sector>
{
    public SectorRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }
} 