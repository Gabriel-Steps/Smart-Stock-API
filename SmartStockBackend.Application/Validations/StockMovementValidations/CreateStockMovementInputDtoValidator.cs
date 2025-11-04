using FluentValidation;
using SmartStockBackend.Application.Services.StockMovementServices.InputModels;

namespace SmartStockBackend.Application.Validations.StockMovementValidations
{
    public class CreateStockMovementInputDtoValidator : AbstractValidator<CreateStockMovementInputDto>
    {
        public CreateStockMovementInputDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("The ProductId must be greater than zero.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid movement type. Accepted values are: Entry, Exit, or Adjustment.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("The quantity must be greater than zero.");

            RuleFor(x => x.MovementDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("The movement date cannot be in the future.");
        }
    }
}
