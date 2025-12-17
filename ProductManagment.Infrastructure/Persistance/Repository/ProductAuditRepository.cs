using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.IRepository;

namespace ProductManagment.Infrastructure.Persistance.Repository
{
    public class ProductAuditRepository : Repository<ProductAudit>, IProductAuditRepository
    {
        private readonly AppDbContext _db;

        public ProductAuditRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
