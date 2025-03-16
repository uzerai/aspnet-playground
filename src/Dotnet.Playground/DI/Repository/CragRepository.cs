using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using NodaTime;

namespace Dotnet.Playground.DI.Repository;

public class CragRepository : EntityRepository<Crag>, ICragRepository
{
    public CragRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }
} 