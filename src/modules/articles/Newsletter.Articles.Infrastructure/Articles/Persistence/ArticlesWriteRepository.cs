using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects;
using Newsletter.Articles.Infrastructure.Articles.Persistence.Errors;
using Newsletter.Articles.Infrastructure.Database;

namespace Newsletter.Articles.Infrastructure.Articles.Persistence;

public sealed class ArticlesWriteRepository : IArticlesWriteRepository
{
    private readonly IArticlesDbContext _dbContext;

    internal ArticlesWriteRepository(IArticlesDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task AddAsync(Article article, CancellationToken cancellationToken = default) =>
        await _dbContext.Articles.AddAsync(article, cancellationToken);

    public void Update(Article article)
    {
        _dbContext.Articles.Attach(article);
        _dbContext.Entry(article).State = EntityState.Modified;
    }

    public async Task<Result> RemoveAsync(ArticleId id, CancellationToken cancellationToken = default)
    {
        Article? article = await _dbContext.Articles
            .AsNoTracking()
            .FirstOrDefaultAsync(article => article.Id == id, cancellationToken);

        if (article is null)
            return Result.Fail(ArticlesPersistenceErrors.ArticleDoesNotExistError);

        _dbContext.Articles.Remove(article);

        return Result.Ok();
    }
}