using MediatR;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Application.Products.Mappers;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<GetProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
            if (product == null)
                throw new KeyNotFoundException($"Product with Id {request.Id} not found.");

            return ProductDtoMapper.ToDto(product);
        }
    }
}