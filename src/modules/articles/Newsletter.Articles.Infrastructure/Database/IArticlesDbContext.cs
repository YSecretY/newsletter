using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newsletter.Articles.Domain.Articles;

namespace Newsletter.Articles.Infrastructure.Database;

internal interface IArticlesDbContext
{
    public DbSet<Article> Articles { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}