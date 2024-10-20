using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        if (exception is LibraryApplicationException applicationException)
        {
            problemDetails.Status = applicationException.ExceptionCode switch
            {
                ExceptionCode.EntityDoesNotExists => StatusCodes.Status404NotFound,
                ExceptionCode.ImpossibleData => StatusCodes.Status422UnprocessableEntity,
                ExceptionCode.AuthenticationError => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status400BadRequest
            };

            problemDetails.Title = applicationException.ExceptionCode.ToString();
            problemDetails.Detail = string.Join(". ", applicationException.ExceptionMessages);
        }
        else
        {
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Title = HttpStatusCode.InternalServerError.ToString();
            problemDetails.Detail = "Unknown error";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}