using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces.IRepository
{
    /// <summary>
    /// Defines repository operations specific to <see cref="ApplicationUser"/> entities.
    /// </summary>
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
    }
}
