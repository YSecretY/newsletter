using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Descriptions;

public sealed class Description : ValueObject
{
    public const int MaxLength = 10_000;

    private Description(string description) =>
        Value = description;

    public string Value { get; }

    public static Result<Description> New(string description)
    {
        if (description.Length > MaxLength)
            return Result.Fail(DescriptionErrors.DescriptionTooLongError);

        if (string.IsNullOrWhiteSpace(description))
            return Result.Fail(DescriptionErrors.DescriptionEmptyError);

        return Result.Ok(new Description(description));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}