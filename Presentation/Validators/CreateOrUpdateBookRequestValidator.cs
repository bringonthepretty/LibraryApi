using FluentValidation;
using Presentation.Requests;

namespace Presentation.Validators;

public class CreateOrUpdateBookRequestValidator: BaseValidator<CreateOrUpdateBookRequest>
{
    public CreateOrUpdateBookRequestValidator()
    {
        RuleFor(book => book.Name)
            .NotNull().WithMessage("Name property cant be null")
            .MinimumLength(5).WithMessage("Name property must be at least 5 characters")
            .MaximumLength(100).WithMessage("Name property must be at most 100 characters");
        RuleFor(book => book.Isbn)
            .NotNull().WithMessage("ISBN property cant be null")
            .MinimumLength(5).WithMessage("ISBN property must be at least 5 characters")
            .MaximumLength(20).WithMessage("ISBN property must be at most 20 characters");
        RuleFor(book => book.Genre)
            .NotNull().WithMessage("Genre property cant be null")
            .MinimumLength(2).WithMessage("Genre property must be at least 2 characters")
            .MaximumLength(50).WithMessage("Genre property must be at most 50 characters");
        RuleFor(book => book.Description)
            .NotNull().WithMessage("Description property cant be null")
            .MaximumLength(1000).WithMessage("Description property must be at most 1000 characters");
        RuleFor(book => book.Image)
            .NotNull().WithMessage("Image property cant be null");
        RuleFor(book => book.AuthorId)
            .NotNull().WithMessage("AuthorId property cant be null");
    }
}