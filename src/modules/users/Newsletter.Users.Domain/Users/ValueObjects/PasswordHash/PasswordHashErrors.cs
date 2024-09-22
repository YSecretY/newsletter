using FluentResults;

namespace Newsletter.Users.Domain.Users.ValueObjects.PasswordHash;

public static class PasswordHashErrors
{
    public static readonly Error PasswordHashTooLongError = new($"Password hash is too long. Max password hash length is {PasswordHash.MaxLength}");
    public static readonly Error PasswordHashEmptyError = new("Password hash cannot be empty.");
}