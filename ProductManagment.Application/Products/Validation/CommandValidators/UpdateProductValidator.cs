using FluentValidation;
using ProductManagment.Application.Products.Commands.UpdateProduct;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Products.Validation.CommandValidators
{
    /// <summary>
    /// Provides validation rules for the <see cref="UpdateProductCommand"/>.
    /// Ensures the product exists and updated fields are valid.
    /// </summary>
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Dto.Id)
                .GreaterThan(0)
                    .WithMessage("Product ID must be greater than 0.")
                 .MustAsync(async (id, cancellation) =>
                 {
                     var product = await _unitOfWork.Products.GetFirstOrDefaultAsync(u => u.Id == id);
                     return product != null;
                 })
                .WithMessage("Product with the specified ID does not exist.");
            RuleFor(x => x.Dto.Title)
                .NotEmpty()
                    .WithMessage("Product title is required.")
                .MaximumLength(100)
                    .WithMessage("Product title cannot exceed 100 characters.");

            RuleFor(x => x.Dto.Quantity)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Quantity must be 0 or greater.");

            RuleFor(x => x.Dto.Price)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Price must be 0 or greater.");
        }
    }
}
