using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.DI.Repository;

public class OrganizationTeamUserRepository : BaseRepository<OrganizationTeamUser>, IOrganizationTeamUserRepository
{
    public OrganizationTeamUserRepository(DatabaseContext context)
        : base(context)
    {
    }
}
