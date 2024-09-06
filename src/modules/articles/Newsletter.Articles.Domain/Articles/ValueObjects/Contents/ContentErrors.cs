using FluentResults;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Contents;

public static class ContentErrors
{
    public static readonly Error ContentTooLongError = new($"Article content is too long. Max content length is: {Content.MaxLength}");
    public static readonly Error ContentEmptyError = new("Article content cannot be empty.");
}