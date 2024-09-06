using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Infrastructure.Articles;

namespace Newsletter.Articles.Infrastructure.Database;

public sealed class ArticlesDbContext(DbContextOptions<ArticlesDbContext> dbContextOptions) : DbContext(dbContextOptions), IArticlesDbContext
{
    public DbSet<Article> Articles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Articles);
        modelBuilder.ApplyConfiguration(new ArticlesConfiguration());
    }
}