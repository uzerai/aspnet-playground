using NodaTime;
using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.DI.Repository;

public class OrganizationRepository : EntityRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }
}