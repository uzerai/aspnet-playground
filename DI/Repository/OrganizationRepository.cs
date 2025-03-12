using NodaTime;
using Uzerai.Dotnet.Playground.DI.Data;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.DI.Repository;

public class OrganizationRepository : BaseRepository<Organization>
{
    public OrganizationRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }
}