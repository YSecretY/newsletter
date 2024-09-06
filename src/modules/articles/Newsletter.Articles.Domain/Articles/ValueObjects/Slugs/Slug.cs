using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Slugs;

public sealed class Slug : ValueObject
{
    public const int MaxLength = 256;

    private Slug(string slug) =>
        Value = slug;

    public string Value { get; }

    public static Result<Slug> New(string slug)
    {
        if (slug.Length > MaxLength)
            return Result.Fail(SlugErrors.SlugTooLongError);

        if (string.IsNullOrWhiteSpace(slug))
            return Result.Fail(SlugErrors.SlugEmptyError);

        if (slug.Any(c => !char.IsLetter(c) && !char.IsDigit(c) && c is not '-'))
            return Result.Fail(SlugErrors.SlugContainsInvalidSymbolsError);

        return Result.Ok(new Slug(slug));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}