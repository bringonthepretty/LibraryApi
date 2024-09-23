using System.Net;
using Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Presentation.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected override void RaiseValidationException(ValidationContext<T> context, ValidationResult validationResult)
    {
        throw new LibraryApplicationException(HttpStatusCode.UnprocessableContent, 
            validationResult.Errors.Select(error => error.ErrorMessage).ToList());
    } 
}