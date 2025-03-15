using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.DI.Repository;

public class OrganizationUserRepository : BaseRepository<OrganizationUser>, IOrganizationUserRepository
{
    public OrganizationUserRepository(DatabaseContext context)
        : base(context)
    {
    }
}