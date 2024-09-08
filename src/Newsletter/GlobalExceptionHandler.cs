using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Newsletter;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = exception switch
        {
            FluentValidation.ValidationException validationException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error",
                Extensions = GetValidationExceptionErrors(validationException)
            },
            _ => new ProblemDetails { Status = StatusCodes.Status500InternalServerError, Title = "Internal server error" }
        };

        if (problemDetails.Status is not null)
            httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static Dictionary<string, object?> GetValidationExceptionErrors(FluentValidation.ValidationException exception)
    {
        Dictionary<string, object?> resultErrors = new();

        foreach (ValidationFailure? error in exception.Errors)
            resultErrors.Add(error.PropertyName, error.ErrorMessage);

        return resultErrors;
    }
}