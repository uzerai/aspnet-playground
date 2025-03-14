
using Dotnet.Playground.DI.Repository.Interface;
using NodaTime;

using Uzerai.Dotnet.Playground.DI.Data;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Uzerai.Dotnet.Playground.DI.Repository;

public class UserRepository : EntityRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext context, IClock clock) : base(context, clock)
    {
    }
}
