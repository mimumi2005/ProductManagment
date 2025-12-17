using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces.IRepository
{
    /// <summary>
    /// Defines repository operations specific to <see cref="Product"/> entities.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Creates a new product including auditing logic
        /// </summary>
        /// <param name="product">The new product entity</param>
        /// <param name="userId">The Id of the user making changes</param>
        /// <returns></returns>
        Task CreateProductAsync(Product product, string userId);

        /// <summary>
        /// Updates a product including auditing logic
        /// </summary>
        /// <param name="product">The updated product entity</param>
        /// <param name="userId">The Id of the user making changes</param>
        /// <returns></returns>
        Task UpdateProductAsync(Product product, string userId);

        /// <summary>
        /// Deletes a product including auditing logic
        /// </summary>
        /// <param name="Id">The unique Id for the product to delete</param>
        /// <param name="userId">The Id of the user making changes</param>
        /// <returns></returns>
        Task DeleteProductAsync(int Id, string userId);
    }
}
