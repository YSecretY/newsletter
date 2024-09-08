using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;
using Newsletter.Articles.Infrastructure.Database;

namespace Newsletter.Articles.Infrastructure.Articles.Persistence;

public sealed class ArticlesReadRepository(IArticlesDbContext dbContext) : IArticlesReadRepository
{
    public async Task<Article?> GetByIdAsync(ArticleId id, CancellationToken cancellationToken = default) =>
        await dbContext.Articles
            .AsNoTracking()
            .FirstOrDefaultAsync(article => article.Id == id, cancellationToken);

    public async Task<List<Article>> GetListAsync(uint pageSize, uint pageNumber, OrderArticlesBy? orderArticlesBy, CancellationToken cancellationToken = default)
    {
        IQueryable<Article> query = dbContext.Articles.AsNoTracking();

        query = orderArticlesBy switch
        {
            OrderArticlesBy.CreatedAt => query.OrderByDescending(a => a.CreatedAt),
            OrderArticlesBy.Popularity => query.OrderByDescending(a => a.ViewsCount),
            _ => query.OrderByDescending(a => a.CreatedAt)
        };

        return await query
            .Skip((int)((pageNumber - 1) * pageSize))
            .Take((int)pageSize)
            .ToListAsync(cancellationToken);
    }
}