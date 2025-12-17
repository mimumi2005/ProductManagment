using MediatR;
using ProductManagment.Application.Products.Dtos;


namespace ProductManagment.Application.Products.Queries.GetProduct
{
    /// <summary>
    /// Query to retrieve all products.
    /// </summary>
    public record GetProductsQuery() : IRequest<List<GetProductDto>>;
}
