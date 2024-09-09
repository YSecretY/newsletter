using Newsletter.Articles.Application.Articles;
using Newsletter.Articles.Domain.Articles;

namespace Newsletter.Articles.Infrastructure.Articles.Persistence;

public sealed class ArticlesUnitOfWork(IArticlesDbContext dbContext) : IArticlesUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}