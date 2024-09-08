using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;
using Newsletter.Articles.Infrastructure.Articles.Persistence.Errors;
using Newsletter.Articles.Infrastructure.Database;

namespace Newsletter.Articles.Infrastructure.Articles.Persistence;

public sealed class ArticlesWriteRepository(IArticlesDbContext dbContext) : IArticlesWriteRepository
{
    public async Task AddAsync(Article article, CancellationToken cancellationToken = default) =>
        await dbContext.Articles.AddAsync(article, cancellationToken);

    public void Update(Article article)
    {
        dbContext.Articles.Attach(article);
        dbContext.Entry(article).State = EntityState.Modified;
    }

    public async Task<Result> RemoveAsync(ArticleId id, CancellationToken cancellationToken = default)
    {
        Article? article = await dbContext.Articles
            .AsNoTracking()
            .FirstOrDefaultAsync(article => article.Id == id, cancellationToken);

        if (article is null)
            return Result.Fail(ArticlesPersistenceErrors.ArticleDoesNotExistError);

        dbContext.Articles.Remove(article);

        return Result.Ok();
    }
}