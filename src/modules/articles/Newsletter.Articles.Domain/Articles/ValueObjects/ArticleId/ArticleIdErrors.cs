using FluentResults;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;

public static class ArticleIdErrors
{
    public static readonly Error InvalidArticleIdError = new("Invalid article id.");
}