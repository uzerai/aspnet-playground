
using Microsoft.EntityFrameworkCore;
using NodaTime;

using Dotnet.Playground.Model;
using Dotnet.Playground.DI.Data;
using Dotnet.Playground.DI.Repository.Interface;

namespace Dotnet.Playground.DI.Repository;

public partial class EntityRepository<T> : BaseRepository<T>, IEntityRepository<T> where T : BaseEntity
{
    private readonly IClock _clock;

    public EntityRepository(DatabaseContext context, IClock clock)
        : base(context)
    {
        _clock = clock;
    }

    public override async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>()
            .AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public override async Task<bool> DeleteAsync(T entity)
    {
        var entityToDelete = await GetByIdTrackingAsync(entity.Id);

        if (entityToDelete == null)
        {
            return false;
        }

        entityToDelete.DeletedAt = _clock.GetCurrentInstant();
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T?> GetByIdTrackingAsync(Guid id)
    {
        return await _context.Set<T>()
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
