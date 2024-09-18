using System.Net;
using Domain.Exceptions;
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
            problemDetails.Status = (int)applicationException.HttpCode;
            problemDetails.Title = applicationException.HttpCode.ToString();
            problemDetails.Detail = string.Join(". ", applicationException.ExceptionMessages);
        }
        else
        {
            problemDetails.Status = (int)HttpStatusCode.InternalServerError;
            problemDetails.Title = HttpStatusCode.InternalServerError.ToString();
            problemDetails.Detail = "Unknown error";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}