using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects;
using Newsletter.Articles.Infrastructure.Database;

namespace Newsletter.Articles.Infrastructure.Articles.Persistence;

public sealed class ArticlesReadRepository : IArticlesReadRepository
{
    private readonly IArticlesDbContext _dbContext;

    internal ArticlesReadRepository(IArticlesDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Article?> GetByIdAsync(ArticleId id, CancellationToken cancellationToken = default) =>
        await _dbContext.Articles
            .AsNoTracking()
            .FirstOrDefaultAsync(article => article.Id == id, cancellationToken);

    public async Task<List<Article>> GetListAsync(uint pageSize, uint pageNumber, OrderArticlesBy? orderArticlesBy, CancellationToken cancellationToken = default)
    {
        IQueryable<Article> query = _dbContext.Articles.AsNoTracking();

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