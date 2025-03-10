using Uzerai.Dotnet.Playground.Model;

namespace Uzerai.Dotnet.Playground.DI.Repository;

public interface IEntityRepository<T> where T : BaseEntity
{

    /// <summary>
    /// Build a queryable collection of entities.
    /// 
    /// Forces tracking of the EF entity.
    /// </summary>
    /// <returns>A queryable collection of entities.</returns>
    IQueryable<T> BuildTrackedQuery();

    /// <summary>
    /// Build a queryable collection of entities.
    /// 
    /// Forces no tracking of the EF entity.
    /// </summary>
    /// <returns>A queryable collection of entities.</returns>
    IQueryable<T> BuildReadonlyQuery();

    /// <summary>
    /// Get an entity by its ID without tracking changes.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Get an entity by its ID with change tracking enabled.
    /// 
    /// Aims to be the only entrypoint to getting a 
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdTrackingAsync(Guid id);

    /// <summary>
    /// Get all entities of type T.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Create a new entity in the database.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity with updated properties (e.g., ID, timestamps).</returns>
    Task<T> CreateAsync(T entity);

    /// <summary>
    /// Update an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The updated entity.</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Soft delete an entity by setting its DeletedAt property.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>True if the entity was found and deleted; otherwise, false.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Save changes made to the database context.
    /// </summary>
    /// <returns>True if any changes were saved; otherwise, false.</returns>
    Task<bool> SaveAsync();
}
