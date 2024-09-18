using System.Net;
using Application.Dtos;
using Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Validators;

public class AuthorValidator : LibraryValidator<AuthorDto>
{
    public AuthorValidator()
    {
        RuleSet("Id", () =>
        {
            RuleFor(author => author.Id).NotNull().WithMessage("Id property cant be null when updating");
        });
        
        RuleSet("NonIdProperties", () =>
        {
            RuleFor(author => author.Name)
                .NotNull().WithMessage("Name property cant be null")
                .MinimumLength(5).WithMessage("Name property must be at least 5 characters")
                .MaximumLength(20).WithMessage("Name property must be at most 20 characters");
            RuleFor(author => author.Surname)
                .NotNull().WithMessage("Surname property cant be null")
                .MinimumLength(5).WithMessage("Surname property must be at least 5 characters")
                .MaximumLength(20).WithMessage("Surname property must be at most 20 characters");
            RuleFor(author => author.BirthDate)
                .NotNull().WithMessage("BirthDate property cant be null")
                .Must(time => time < DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).WithMessage("Author must be at least one year old :)");
            RuleFor(author => author.Country)
                .NotNull().WithMessage("Country property cant be null")
                .MinimumLength(3).WithMessage("Country property must be at least 3 characters")
                .MaximumLength(20).WithMessage("Country property must be at most 20 characters");
        });
        
    
    }
    
    protected override void RaiseValidationException(ValidationContext<AuthorDto> context, ValidationResult validationResult)
    {
        throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, 
            validationResult.Errors.Select(error => error.ErrorMessage).ToList());
    } 
}