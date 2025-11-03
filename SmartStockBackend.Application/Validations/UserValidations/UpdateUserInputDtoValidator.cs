using FluentValidation;
using SmartStockBackend.Application.Services.UserServices.InputModels;

namespace SmartStockBackend.Application.Validations.UserValidations
{
    public class UpdateUserInputDtoValidator : AbstractValidator<UpdateUserInputDto>
    {
        public UpdateUserInputDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MinimumLength(3).WithMessage("Name must have at least 3 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone cannot be empty.")
                .Matches(@"^\+?\d{8,15}$").WithMessage("Phone must be a valid number.");

            RuleFor(x => x.ImageUrl)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                .When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("Invalid image URL format.");
        }
    }
}
