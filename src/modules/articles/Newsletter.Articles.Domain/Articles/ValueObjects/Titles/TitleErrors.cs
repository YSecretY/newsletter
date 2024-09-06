using FluentResults;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Titles;

public static class TitleErrors
{
    public static readonly Error TitleTooLongError = new($"Title is too long. Max title length is {Title.MaxLength}");
    public static readonly Error TitleEmptyError = new("Title cannot be empty.");
}