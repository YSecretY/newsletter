using FluentResults;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Slugs;

public static class SlugErrors
{
    public static readonly Error SlugTooLongError = new($"Article slug is too long. Max tag length is: {Slug.MaxLength}");
    public static readonly Error SlugEmptyError = new("Article slug cannot be empty.");
    public static readonly Error SlugContainsInvalidSymbolsError = new("Article slug contains invalid symbols. Valid symbols are: characters, digits, -(dashes)");
}