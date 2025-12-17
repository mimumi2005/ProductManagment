using MediatR;
using ProductManagment.Application.Products.Dtos;


namespace ProductManagment.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Represents a command to update a product entity.
    /// </summary>
    /// <param name="Dto">The data transfer object containing phase creation information.</param>
    public record UpdateProductCommand(ProductDto Dto, string UserId) : IRequest<GetProductDto>
    {
    }
}
