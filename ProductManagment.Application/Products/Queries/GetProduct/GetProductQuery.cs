using MediatR;
using ProductManagment.Application.Products.Dtos;


namespace ProductManagment.Application.Products.Queries.GetProduct
{
    /// <summary>
    /// Query to retrieve a product by id.
    /// </summary>
    public record GetProductQuery(int Id) : IRequest<GetProductDto>;
}
