
using Dotnet.Playground.DI.Repository.Interface;
using NodaTime;

using Dotnet.Playground.DI.Data;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Repository;

public class UserRepository : EntityRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext context, IClock clock) : base(context, clock)
    {
    }
}
