using MediatR;
using ProductManagment.Application.Common;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Application.Products.Mappers
{
    public class ProductDtoMapper
    {
        /// <summary>
        /// Converts a <see cref="Product"/> entity to its corresponding <see cref="GetProductDto"/>.
        /// </summary>
        /// <param name="Product">The phase entity to convert.</param>
        /// <returns>A <see cref="GetProductDto"/> representation of the entity.</returns>
        public static GetProductDto ToDto(Product product)
        {
            if (product == null) return null;
            return new GetProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Quantity = product.Quantity,
                Price = product.Price,
                TotalPriceWithVAT = (product.Price * product.Quantity) * (1 + PriceConfig.VAT)
            };
        }

        /// <summary>
        /// Converts a <see cref="ProductDto"/> to its corresponding <see cref="Product"/> entity.
        /// </summary>
        /// <param name="dto">The DTO to convert.</param>
        /// <returns>A <see cref="Product"/> entity representation of the DTO.</returns>
        public static Product? ToEntity(ProductDto dto)
        {
            if (dto == null) return null;
            return new Product
            {
                Title = dto.Title,
                Quantity = dto.Quantity,
                Price = dto.Price,
            };
        }
    }
}
