using FluentResults;

namespace Newsletter.Shared.Domain.Errors;

public sealed class StatusError(StatusCode statusCode, string message) : Error(message)
{
    public StatusCode StatusCode { get; } = statusCode;
}