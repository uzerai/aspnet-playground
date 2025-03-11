
using Microsoft.EntityFrameworkCore;
using NodaTime;

using Uzerai.Dotnet.Playground.Model;
using Uzerai.Dotnet.Playground.DI.Data;

namespace Uzerai.Dotnet.Playground.DI.Repository;

public partial class BaseRepository<T> : IEntityRepository<T> where T : BaseEntity
{
    private readonly DatabaseContext _context;
    private readonly IClock _clock;

    public BaseRepository(DatabaseContext context, IClock clock)
    {
        _context = context;
        _clock = clock;
    }

    public IQueryable<T> BuildTrackedQuery()
    {
        return _context.Set<T>()
            .AsTracking()
            .AsQueryable();
    }

    public IQueryable<T> BuildReadonlyQuery()
    {
        return _context.Set<T>()
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>()
            .AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
        {
            return false;
        }

        entity.DeletedAt = _clock.GetCurrentInstant();
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
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

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}
