using FluentResults;
using Newsletter.Shared.Domain.Errors;

namespace Newsletter.Articles.Domain.Articles;

public static class ArticlesErrors
{
    public static readonly Error ArticleCreatedInFutureError = new("Article created at time cannot be later than now.");
    public static readonly StatusError ArticleIsNotFoundError = new(StatusCode.NotFound, "Article is not found.");
}