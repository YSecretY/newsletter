using FluentResults;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.Descriptions;

public static class DescriptionErrors
{
    public static readonly Error DescriptionTooLongError = new($"Article description is too long. Max description length is: {Description.MaxLength}");
    public static readonly Error DescriptionEmptyError = new("Article description cannot be empty.");
}