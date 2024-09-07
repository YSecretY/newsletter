using Newsletter.Articles.Application.Articles;
using Newsletter.Articles.Infrastructure.Database;

namespace Newsletter.Articles.Infrastructure.Articles.Persistence;

public sealed class ArticlesUnitOfWork : IArticlesUnitOfWork
{
    private readonly IArticlesDbContext _dbContext;

    internal ArticlesUnitOfWork(IArticlesDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}