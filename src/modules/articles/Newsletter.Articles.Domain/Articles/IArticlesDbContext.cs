using Microsoft.EntityFrameworkCore;

namespace Newsletter.Articles.Domain.Articles;

public interface IArticlesDbContext
{
    public DbSet<Article> Articles { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}