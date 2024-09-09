using FluentResults;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;

namespace Newsletter.Articles.Application.Articles.Repositories;

public interface IArticlesWriteRepository
{
    public Task AddAsync(Article article, CancellationToken cancellationToken = default);

    public void Update(Article article);

    public Task<Result> RemoveAsync(ArticleId id, CancellationToken cancellationToken = default);
}