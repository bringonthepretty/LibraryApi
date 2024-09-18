using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class UserValidator : LibraryValidator<UserDto>
{
    public UserValidator()
    {
        RuleSet("Id", () =>
        {
            RuleFor(user => user.Id).NotNull().WithMessage("Id property cant be null when updating");
        });

        RuleSet("NonIdProperties", () =>
        {
            RuleFor(user => user.Username)
                .NotNull().WithMessage("Username property cant be null")
                .MinimumLength(5).WithMessage("Username property must be at least 5 characters")
                .MaximumLength(50).WithMessage("Username property must be at most 50 characters");
            RuleFor(user => user.Login)
                .NotNull().WithMessage("Login property cant be null")
                .MinimumLength(8).WithMessage("Login property must be at least 8 characters")
                .MaximumLength(30).WithMessage("Login property must be at most 30 characters");
            RuleFor(user => user.Role)
                .NotNull().WithMessage("Role property cant be null")
                .MinimumLength(2).WithMessage("Role property must be at least 2 characters")
                .MaximumLength(30).WithMessage("Role property must be at most 30 characters");
        });
    }
}