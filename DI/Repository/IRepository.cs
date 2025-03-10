using Playground.Model;

namespace Playground.DI.Repository;

public interface IEntityRepository<T> where T : BaseEntity
{
    IQueryable<T> BuildQuery();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetByIdTrackingAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> SaveAsync();
}
