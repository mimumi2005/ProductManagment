using System.Linq.Expressions;

namespace ProductManagment.Domain.Interfaces.IRepository
{

    /// <summary>
    /// Defines a generic repository contract that provides common data access operations
    /// for entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves an entity by its identifier, or <c>null</c> if not found.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity of type <typeparamref name="T"/> if found; otherwise, <c>null</c>.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all entities matching the specified predicate, including related entities if specified.
        /// </summary>
        /// <param name="predicate">An optional filter expression.</param>
        /// <param name="includeProperties">Comma-separated list of related entities to include.</param>
        /// <returns>A collection of entities of type <typeparamref name="T"/>.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string? includeProperties = null);

        /// <summary>
        /// Retrieves all entities matching the specified predicate, including related entities if specified.
        /// </summary>
        /// <param name="predicate">The filter expression.</param>
        /// <param name="asNoTracking">Whether to disable entity tracking for performance.</param>
        /// <param name="includeProperties">Navigation properties to include.</param>
        /// <returns>A collection of entities of type <typeparamref name="T"/>.</returns>
        Task<IEnumerable<T>> GetAllIncludingAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Removes an entity from the repository by its identifier.
        /// </summary>
        /// <param name="entityId">The identifier of the entity to remove.</param>
        Task RemoveAsync(int entityId);

        /// <summary>
        /// Retrieves the first entity matching the specified filter, or <c>null</c> if none found.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <param name="includeProperties">Optional related entities to include.</param>
        /// <param name="tracked">Whether to track the entity in the context.</param>
        /// <returns>The entity of type <typeparamref name="T"/> if found; otherwise, <c>null</c>.</returns>
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
    }
}
