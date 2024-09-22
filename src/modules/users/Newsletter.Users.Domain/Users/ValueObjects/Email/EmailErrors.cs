using FluentResults;

namespace Newsletter.Users.Domain.Users.ValueObjects.Email;

public static class EmailErrors
{
    public static readonly Error InvalidEmailError = new("Email is invalid.");
}