using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Repository.Interface;

public interface INoteRepository : IEntityRepository<Note>
{
    // Add any Note-specific repository methods here
} 