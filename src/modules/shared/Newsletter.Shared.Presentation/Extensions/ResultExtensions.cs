using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Shared.Domain.Errors;

namespace Newsletter.Shared.Presentation.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Result result) =>
        result.IsSuccess
            ? new OkResult()
            : new BadRequestObjectResult(new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Detail = string.Join("\n", result.Errors.Select(e => e.Message))
            });


    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        StatusError? statusError = result.Errors.OfType<StatusError>().FirstOrDefault();

        return result.IsSuccess
            ? new OkObjectResult(result.Value)
            : new BadRequestObjectResult(new ProblemDetails
            {
                Status = statusError != null ? (int)statusError.StatusCode : (int)HttpStatusCode.BadRequest,
                Detail = string.Join("\n", result.Errors.Select(e => e.Message))
            });
    }
}