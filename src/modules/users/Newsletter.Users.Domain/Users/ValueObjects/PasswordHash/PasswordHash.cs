using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Users.Domain.Users.ValueObjects.PasswordHash;

public sealed class PasswordHash : ValueObject
{
    public const int MaxLength = 256;

    private PasswordHash(string value) =>
        Value = value;

    public string Value { get; }

    public static Result<PasswordHash> New(string password)
    {
        if (password.Length > MaxLength)
            return Result.Fail(PasswordHashErrors.PasswordHashTooLongError);

        if (string.IsNullOrWhiteSpace(password))
            return Result.Fail(PasswordHashErrors.PasswordHashEmptyError);

        return Result.Ok(new PasswordHash(password));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}