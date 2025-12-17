using MediatR;
using ProductManagment.Application.Common;


namespace ProductManagment.Application.Products.Commands.DeleteProduct
{
    /// <summary>
    /// Represents a command to delete a product entity.
    /// </summary>
    /// <param name="Id">The Id of the product to be deleted.</param>
    public record DeleteProductCommand(int Id, string UserId) : IRequest<ApiResponse>
    {
    }
}
