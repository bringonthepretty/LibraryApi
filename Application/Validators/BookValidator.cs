using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class BookValidator : LibraryValidator<BookDto>
{
    public BookValidator()
    {
        RuleSet("Id", () =>
        {
            RuleFor(book => book.Id).NotNull().WithMessage("Id property cant be null when updating");
        });
        
        RuleSet("NonIdProperties", () =>
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
            RuleFor(book => book.Available)
                .NotNull().WithMessage("Available property cant be null");
            // RuleFor(book => book.BorrowTime)
            //     .Must(time => time < DateTime.Now.AddHours(1)).When(time => time is not null)
            //     .WithMessage("BorrowTime property must be in the past if set");
            RuleFor(book => book.Image)
                .NotNull().WithMessage("Image property cant be null");
            RuleFor(book => book.AuthorId)
                .NotNull().WithMessage("AuthorId property cant be null");
        });
    }
}