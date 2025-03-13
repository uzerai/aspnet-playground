using Uzerai.Dotnet.Playground.DI.Data;
using Uzerai.Dotnet.Playground.Model.Tags;

namespace Uzerai.Dotnet.Playground.DI.Repository;

public class TagRepository : BaseRepository<Tag>
{
    public TagRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}
