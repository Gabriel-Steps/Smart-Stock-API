using FluentValidation;
using SmartStockBackend.Application.Services.ProductServices.InputModels;

namespace SmartStockBackend.Application.Validations.ProductValidations
{
    public class CreateProductInputValidatorDto : AbstractValidator<CreateProductInputDto>
    {
        public CreateProductInputValidatorDto()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(3).WithMessage("Product name must have at least 3 characters.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.SKU)
                .NotEmpty().WithMessage("SKU is required.")
                .Matches(@"^[A-Z0-9\-]+$").WithMessage("SKU must contain only uppercase letters, numbers, and hyphens.")
                .MaximumLength(30).WithMessage("SKU cannot exceed 30 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.MinimumQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum quantity cannot be negative.")
                .LessThanOrEqualTo(x => x.Quantity)
                .WithMessage("Minimum quantity cannot be greater than current quantity.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .LessThan(1000000).WithMessage("Price cannot exceed 1,000,000.");
        }
    }
}
