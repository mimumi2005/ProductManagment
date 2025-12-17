using MediatR;
using ProductManagment.Application.Products.Dtos;

namespace ProductManagment.Application.Products.Commands.CreateProduct
{
    /// <summary>
    /// Represents a command to create a new product entity.
    /// </summary>
    /// <param name="Dto">The data transfer object containing phase creation information.</param>
    public record CreateProductCommand(ProductDto Dto, string UserId) : IRequest<GetProductDto>
    {
    }
}
