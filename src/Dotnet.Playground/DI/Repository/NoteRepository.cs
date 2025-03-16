using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Repository;

public class NoteRepository : EntityRepository<Note>, INoteRepository
{
    public NoteRepository(DatabaseContext context)
        : base(context)
    {
    }
} 