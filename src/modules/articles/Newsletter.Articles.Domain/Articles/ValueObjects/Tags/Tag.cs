using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Tags;

public class Tag : ValueObject
{
    public const int MaxLength = 512;

    private Tag(string tag) =>
        Value = tag;

    public string Value { get; }

    public static Result<Tag> New(string tag)
    {
        if (tag.Length > MaxLength)
            return Result.Fail(TagErrors.TagTooLongError);

        if (string.IsNullOrWhiteSpace(tag))
            return Result.Fail(TagErrors.TagEmptyError);

        if (tag.Any(c => !char.IsDigit(c) && !char.IsLetter(c) && c is not '_' && c is not '#' && c is not '-'))
            return Result.Fail(TagErrors.TagContainsInvalidSymbolsError);

        return Result.Ok(new Tag(tag));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}