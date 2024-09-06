using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Contents;

public sealed class Content : ValueObject
{
    public const int MaxLength = 1000_000;

    private Content(string content) =>
        Value = content;

    public static Result<Content> New(string content)
    {
        if (content.Length > MaxLength)
            return Result.Fail(ContentErrors.ContentTooLongError);

        if (string.IsNullOrWhiteSpace(content))
            return Result.Fail(ContentErrors.ContentEmptyError);

        return Result.Ok(new Content(content));
    }

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}