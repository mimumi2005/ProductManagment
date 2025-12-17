using FluentValidation;
using ProductManagment.Application.Products.Commands.DeleteProduct;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Products.Validation.CommandValidators
{
    /// <summary>
    /// Provides validation rules for the <see cref="DeleteProductCommand"/>.
    /// Ensures the product exists before deletion.
    /// </summary>
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("Product ID must be greater than 0.")
                .MustAsync(async (id, cancellation) =>
                {
                    var product = await _unitOfWork.Products.GetFirstOrDefaultAsync(u => u.Id == id);
                    return product != null;
                })
                .WithMessage("Product with the specified ID does not exist.");
        }
    }
}
