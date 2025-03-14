using Uzerai.Dotnet.Playground.DI.Data;
using Uzerai.Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.DI.Repository;

public class OrganizationUserRepository : BaseRepository<OrganizationUser>, IOrganizationUserRepository
{
    public OrganizationUserRepository(DatabaseContext context)
        : base(context)
    {
    }
}