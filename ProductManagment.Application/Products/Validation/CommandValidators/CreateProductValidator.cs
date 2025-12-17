using FluentValidation;
using ProductManagment.Application.Products.Commands.CreateProduct;

namespace ProductManagment.Application.Products.Validation.CommandValidators
{
    /// <summary>
    /// Provides validation rules for the <see cref="CreateProductCommand"/>.
    /// Ensures required fields are present, value ranges are valid,
    /// and length constraints are respected.
    /// </summary>
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
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
