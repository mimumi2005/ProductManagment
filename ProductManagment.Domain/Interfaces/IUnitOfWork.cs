using ProductManagment.Domain.Interfaces.IRepository;

namespace ProductManagment.Domain.Interfaces
{   
     /// <summary>
     /// Defines a unit of work contract that provides access to repositories
     /// and manages database transactions.
     /// </summary>
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IProductRepository Products { get; }
        IProductAuditRepository ProductAudits { get; }

        /// <summary>
        /// Commits the current database transaction asynchronously.
        /// </summary>
        Task CommitAsync();
    }
}
