using Microsoft.EntityFrameworkCore;
using NodaTime;
using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.DI.Repository;

public class OrganizationTeamRepository : EntityRepository<OrganizationTeam>, IOrganizationTeamRepository
{
    public OrganizationTeamRepository(DatabaseContext context, IClock clock)
        : base(context, clock)
    {
    }

    public async Task<IEnumerable<OrganizationTeam>> GetAllForOrganizationAsync(Guid organizationId)
    {
        return await BuildReadonlyQuery()
            .Where(e => e.OrganizationId == organizationId)
            .ToListAsync();
    }
}