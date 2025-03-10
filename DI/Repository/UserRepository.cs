
using NodaTime;
using Playground.Model.Authentication;

namespace Playground.DI.Repository;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(DatabaseContext context, IClock clock) : base(context, clock)
    {
    }
}
