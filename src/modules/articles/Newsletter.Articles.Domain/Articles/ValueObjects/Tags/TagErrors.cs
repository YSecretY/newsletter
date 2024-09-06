using FluentResults;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Tags;

public static class TagErrors
{
    public static readonly Error TagTooLongError = new($"Article tag is too long. Max tag length is: {Tag.MaxLength}");
    public static readonly Error TagEmptyError = new("Article tag cannot be empty.");
    public static readonly Error TagContainsInvalidSymbolsError = new("Article tag contains invalid symbols. Valid symbols are: characters, digits, _(underscores), #(hashtags), -(dashes)");
}