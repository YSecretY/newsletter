using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Titles;

public sealed class Title : ValueObject
{
    public const int MaxLength = 2056;

    private Title(string title) =>
        Value = title;

    public string Value { get; }

    public static Result<Title> New(string title)
    {
        if (title.Length > MaxLength)
            return Result.Fail(TitleErrors.TitleTooLongError);

        if (string.IsNullOrWhiteSpace(title))
            return Result.Fail(TitleErrors.TitleEmptyError);

        return Result.Ok(new Title(title));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}