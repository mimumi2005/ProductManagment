using MediatR;
using ProductManagment.Application.Common;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Products.Commands.DeleteProduct
{
    /// <summary>
    /// Handles the <see cref="DeleteProductCommand"/> by deleting an existing <see cref="Product"/>
    /// entity and returning an api response.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Products.DeleteProductAsync(request.Id, request.UserId);
            await _unitOfWork.CommitAsync();
            return new ApiResponse("Product deleted succesfully", request.Id);
        }
    }
}
