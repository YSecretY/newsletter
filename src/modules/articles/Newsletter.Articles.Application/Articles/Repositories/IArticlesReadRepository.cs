using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;

namespace Newsletter.Articles.Application.Articles.Repositories;

public interface IArticlesReadRepository
{
    public Task<Article?> GetByIdAsync(ArticleId id, CancellationToken cancellationToken = default);

    public Task<List<Article>> GetListAsync(uint pageSize, uint pageNumber, OrderArticlesBy? orderArticlesBy = OrderArticlesBy.CreatedAt,
        CancellationToken cancellationToken = default);
}