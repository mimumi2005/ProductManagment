using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.IRepository;

namespace ProductManagment.Infrastructure.Persistance
{
    /// <summary>
    /// Implements the Unit of Work pattern, coordinating multiple repositories and
    /// managing transactions across the application's database context.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public IApplicationUserRepository ApplicationUser { get; }
        public IProductRepository Products { get; }
        public IProductAuditRepository ProductAudits { get; }

        private readonly AppDbContext _db;

        public UnitOfWork(
            AppDbContext db,
            IApplicationUserRepository applicationUserRepository,
            IProductRepository productRepository,
            IProductAuditRepository productAuditRepository)
        {
            _db = db;
            ApplicationUser = applicationUserRepository;
            Products = productRepository;
            ProductAudits = productAuditRepository;
        }

        /// <inheritdoc />
        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}