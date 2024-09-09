using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Shared.Domain.Errors;

namespace Newsletter.Shared.Presentation.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Result result)
    {
        StatusError? statusError = result.Errors.OfType<StatusError>().FirstOrDefault();

        if (statusError != null)
        {
            return new ObjectResult(new ProblemDetails
            {
                Status = (int)statusError.StatusCode,
                Detail = string.Join("\n", result.Errors.Select(e => e.Message))
            })
            {
                StatusCode = (int)statusError.StatusCode
            };
        }

        return new BadRequestObjectResult(new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Detail = string.Join("\n", result.Errors.Select(e => e.Message))
        });
    }

    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        StatusError? statusError = result.Errors.OfType<StatusError>().FirstOrDefault();

        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        if (statusError != null)
        {
            return new ObjectResult(new ProblemDetails
            {
                Status = (int)statusError.StatusCode,
                Detail = string.Join("\n", result.Errors.Select(e => e.Message))
            })
            {
                StatusCode = (int)statusError.StatusCode
            };
        }

        return new BadRequestObjectResult(new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Detail = string.Join("\n", result.Errors.Select(e => e.Message))
        });
    }
}
