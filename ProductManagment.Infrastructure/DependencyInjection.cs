using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.IRepository;
using ProductManagment.Infrastructure.Persistance;
using ProductManagment.Infrastructure.Persistance.Repository;

namespace ProductManagment.Infrastructure
{
    /// <summary>
    /// This class is used to register the infrastructure services in the DI container.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<AppDbContext>());

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAuditRepository, ProductAuditRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }

}
