using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Location;
using NodaTime;

namespace Dotnet.Playground.DI.Repository;

public class PitchRepository : EntityRepository<Pitch>, IPitchRepository
{
    public PitchRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }
} 