using MediatR;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Application.Products.Mappers;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Handles the <see cref="UpdateProductCommand"/> by updating a <see cref="Product"/>
    /// entity and returning its corresponding <see cref="ProductDto"/>.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GetProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<GetProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ProductDtoMapper.ToEntity(request.Dto);

            await _unitOfWork.Products.UpdateProductAsync(product, request.UserId);
            await _unitOfWork.CommitAsync();
            return ProductDtoMapper.ToDto(product);
        }
    }
}
