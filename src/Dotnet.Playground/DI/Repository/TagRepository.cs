using NodaTime;
using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Tags;

namespace Dotnet.Playground.DI.Repository;

public class TagRepository : EntityRepository<Tag>, ITagRepository
{
    public TagRepository(DatabaseContext databaseContext, IClock clock) : base(databaseContext, clock)
    {
    }
}
