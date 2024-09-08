using FluentResults;

namespace Newsletter.Articles.Infrastructure.Articles.Persistence.Errors;

public static class ArticlesPersistenceErrors
{
    public static readonly Error ArticleDoesNotExistError = new("Article does not exist.");
}