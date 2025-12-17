using MediatR;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Application.Products.Mappers;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Products.Commands.CreateProduct
{
    /// <summary>
    /// Handles the <see cref="CreateProductCommand"/> by creating a new <see cref="Product"/>
    /// entity and returning its corresponding <see cref="ProductDto"/>.
    /// </summary>
    public class CreatePhaseHandler : IRequestHandler<CreateProductCommand, GetProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePhaseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<GetProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ProductDtoMapper.ToEntity(request.Dto);
            await _unitOfWork.Products.CreateProductAsync(product, request.UserId);
            await _unitOfWork.CommitAsync();
            return ProductDtoMapper.ToDto(product);
        }
    }
}
