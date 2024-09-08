namespace Newsletter.Articles.Application.Articles;

public interface IArticlesUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}