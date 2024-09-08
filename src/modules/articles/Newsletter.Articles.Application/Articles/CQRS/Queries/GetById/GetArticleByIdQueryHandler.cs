using FluentResults;
using MediatR;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;

namespace Newsletter.Articles.Application.Articles.CQRS.Queries.GetById;

public sealed class GetArticleByIdQueryHandler(IArticlesReadRepository articlesReadRepository) : IRequestHandler<GetArticleByIdQuery, Result<ArticleDto>>
{
    public async Task<Result<ArticleDto>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        Result<ArticleId> articleIdResult = ArticleId.From(request.ArticleId);
        if (articleIdResult.IsFailed)
            return Result.Fail(articleIdResult.Errors);

        Article? article = await articlesReadRepository.GetByIdAsync(articleIdResult.Value, cancellationToken);
        if (article is null)
            return Result.Fail(ArticlesErrors.ArticleIsNotFoundError);

        return ArticleDto.From(article);
    }
}