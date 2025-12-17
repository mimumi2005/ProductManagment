using MediatR;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Application.Products.Mappers;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Products.Queries.GetProduct
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<List<GetProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return products.Select(ProductDtoMapper.ToDto).ToList();
        }
    }   
}