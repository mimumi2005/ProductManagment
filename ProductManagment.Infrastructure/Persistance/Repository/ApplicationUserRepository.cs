using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.IRepository;

namespace ProductManagment.Infrastructure.Persistance.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly AppDbContext _db;

        public ApplicationUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
