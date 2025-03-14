using NodaTime;
using Uzerai.Dotnet.Playground.DI.Data;
using Uzerai.Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.Model.Tags;

namespace Uzerai.Dotnet.Playground.DI.Repository;

public class TagRepository : EntityRepository<Tag>, ITagRepository
{
    public TagRepository(DatabaseContext databaseContext, IClock clock) : base(databaseContext, clock)
    {
    }
}
