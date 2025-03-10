using Playground.Model;

namespace Playground.DI.Repository;

public interface IEntityRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdTrackingAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> SaveAsync();
}
