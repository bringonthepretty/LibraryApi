using Application.DependencyInjectionExtensions;
using FluentValidation;
using Presentation.Requests;

namespace Presentation.Validators;

[Service]
public class RegisterRequestValidator: BaseValidator<RegisterWebRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(user => user.Username)
            .NotNull().WithMessage("Username property cant be null")
            .MinimumLength(5).WithMessage("Username property must be at least 5 characters")
            .MaximumLength(50).WithMessage("Username property must be at most 50 characters");
        RuleFor(user => user.Login)
            .NotNull().WithMessage("Login property cant be null")
            .MinimumLength(8).WithMessage("Login property must be at least 8 characters")
            .MaximumLength(30).WithMessage("Login property must be at most 30 characters");
        RuleFor(user => user.Password)
            .NotNull().WithMessage("Password property cant be null")
            .MinimumLength(2).WithMessage("Password property must be at least 8 characters")
            .MaximumLength(30).WithMessage("Password property must be at most 30 characters");
    }
}