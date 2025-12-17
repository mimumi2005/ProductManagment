using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Interfaces.IRepository;
using System.Linq.Expressions;

namespace ProductManagment.Infrastructure.Persistance.Repository
{

/// <summary>
/// Generic repository implementation for entities of type <typeparamref name="T"/>,
/// providing common data access methods such as retrieval, addition, update,
/// and deletion. Implements <see cref="IRepository{T}"/>.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(AppDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }

    /// <inheritdoc />
    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
    {
        if (tracked)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
        else
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return await query.ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task RemoveAsync(int entityId)
    {
        var entity = await dbSet.FindAsync(entityId);
        if (entity != null)
        {
            dbSet.Remove(entity);
        }
        else
        {
            throw new KeyNotFoundException($"Entity with ID {entityId} was not found.");
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAllIncludingAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = dbSet;

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }
}
}
