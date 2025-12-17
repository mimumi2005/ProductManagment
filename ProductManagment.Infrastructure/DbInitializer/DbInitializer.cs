using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagment.Domain.Entities;
using ProductManagment.Infrastructure.Persistance;

namespace ProductManagment.Infrastructure.DbInitializer
{
    /// <summary>
    /// Seeds default roles, users, and products.
    /// </summary>
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
                return;

            await context.Database.MigrateAsync();

            // Seeding roles
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            // Seeding admin user
            if (await userManager.FindByEmailAsync("admin@example.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seeding default user
            if (await userManager.FindByEmailAsync("user@example.com") == null)
            {
                var normalUser = new ApplicationUser
                {
                    UserName = "user@example.com",
                    Email = "user@example.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(normalUser, "User123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }

            // Seeding products
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Title = "Shampoo",
                        Quantity = 50,
                        Price = 5.99m
                    },
                    new Product
                    {
                        Title = "Car Wax",
                        Quantity = 25,
                        Price = 12.49m
                    },
                    new Product
                    {
                        Title = "Microfiber Cloth",
                        Quantity = 100,
                        Price = 2.99m
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
