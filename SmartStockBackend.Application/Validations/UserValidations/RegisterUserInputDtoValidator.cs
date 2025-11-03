using FluentValidation;
using SmartStockBackend.Application.Services.UserServices.InputModels;

namespace SmartStockBackend.Application.Validations.UserValidations
{
    public class RegisterUserInputDtoValidator : AbstractValidator<RegisterUserInputDto>
    {
        public RegisterUserInputDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must have at least 3 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\+?\d{8,15}$").WithMessage("Phone must be a valid number.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.ImageUrl)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                .When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("Invalid image URL format.");
        }
    }
}
