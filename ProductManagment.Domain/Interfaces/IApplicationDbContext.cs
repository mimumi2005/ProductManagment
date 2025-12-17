using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for the application's database context,
    /// providing access to entity sets and database operations.
    /// </summary>
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; }
        DbSet<Product> Products { get; }
        DbSet<ProductAudit> Product_audits { get; }

        /// <summary>
        /// Persists changes made in this context to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
