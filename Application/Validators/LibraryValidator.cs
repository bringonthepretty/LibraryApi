using System.Net;
using Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Validators;

public abstract class LibraryValidator<T> : AbstractValidator<T>
{
    protected override void RaiseValidationException(ValidationContext<T> context, ValidationResult validationResult)
    {
        throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, 
            validationResult.Errors.Select(error => error.ErrorMessage).ToList());
    } 
}