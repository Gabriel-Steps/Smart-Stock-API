using FluentValidation;
using SmartStockBackend.Application.Services.UserServices.InputModels;

namespace SmartStockBackend.Application.Validations.UserValidations
{
    public class LoginUserInputDtoValidator : AbstractValidator<LoginUserInputDto>
    {
        public LoginUserInputDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
