using FluentResults;

namespace Newsletter.Users.Domain.Users.ValueObjects.FirstName;

public static class FirstNameErrors
{
    public static readonly Error FirstNameTooLongError = new($"First name is too long. Maximum first name length is {FirstName.MaxLength}.");
    public static readonly Error FirstNameEmptyError = new("First name cannot be empty.");
}