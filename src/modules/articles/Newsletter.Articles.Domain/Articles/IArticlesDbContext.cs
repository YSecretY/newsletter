using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Newsletter.Articles.Domain.Articles;

public interface IArticlesDbContext
{
    public DbSet<Article> Articles { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}