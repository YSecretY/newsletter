using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Users.Domain.Users.ValueObjects.FirstName;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 256;

    private FirstName(string value)
        => Value = value;

    public string Value { get; }


    public static Result<FirstName> New(string firstName)
    {
        if (firstName.Length > MaxLength)
            return Result.Fail(FirstNameErrors.FirstNameTooLongError);

        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Fail(FirstNameErrors.FirstNameEmptyError);

        return Result.Ok(new FirstName(firstName));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}