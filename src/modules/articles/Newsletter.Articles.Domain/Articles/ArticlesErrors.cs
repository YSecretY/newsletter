using FluentResults;

namespace Newsletter.Articles.Domain.Articles;

public static class ArticlesErrors
{
    public static readonly Error ArticleCreatedInFutureError = new Error("Article created at time cannot be later than now.");
}