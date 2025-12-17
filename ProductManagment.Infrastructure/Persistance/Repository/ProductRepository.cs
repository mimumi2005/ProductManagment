using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.IRepository;
using System.Text.Json;

namespace ProductManagment.Infrastructure.Persistance.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateProductAsync(Product product, string userId)
        {
            var newValues = JsonSerializer.Serialize(product);

            _db.Product_audits.Add(new ProductAudit
            {
                ProductId = product.Id,
                UserId = userId,
                ChangeType = "Create",
                ChangeDate = DateTime.UtcNow,
                OldValues = "",
                NewValues = newValues,
            });
            _db.Products.Add(product);

            await _db.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product, string userId)
        {
            var existing = await _db.Products.FindAsync(product.Id);

            var oldValues = JsonSerializer.Serialize(existing);

            existing.Title = product.Title;
            existing.Quantity = product.Quantity;
            existing.Price = product.Price;

            var newValues = JsonSerializer.Serialize(existing);

            _db.Product_audits.Add(new ProductAudit
            {
                ProductId = product.Id,
                UserId = userId,
                ChangeType = "Update",
                ChangeDate = DateTime.UtcNow,
                OldValues = oldValues,  
                NewValues = newValues,
            });
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int Id, string userId)
        {
            var existing = await _db.Products.FindAsync(Id);

            var oldValues = JsonSerializer.Serialize(existing);

            _db.Product_audits.Add(new ProductAudit
            {
                ProductId = Id,
                UserId = userId,
                ChangeType = "Delete",
                ChangeDate = DateTime.UtcNow,
                OldValues = oldValues,
                NewValues = "",
            });

            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();
        }

    }
}
