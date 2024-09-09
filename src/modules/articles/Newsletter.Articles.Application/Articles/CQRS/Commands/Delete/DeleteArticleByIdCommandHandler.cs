using FluentResults;
using MediatR;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;

namespace Newsletter.Articles.Application.Articles.CQRS.Commands.Delete;

public sealed class DeleteArticleByIdCommandHandler(
    IArticlesWriteRepository articlesWriteRepository,
    IArticlesUnitOfWork unitOfWork
) : IRequestHandler<DeleteArticleByIdCommand, Result>
{
    public async Task<Result> Handle(DeleteArticleByIdCommand request, CancellationToken cancellationToken)
    {
        Result<ArticleId> articleIdResult = ArticleId.From(request.ArticleId);

        if (articleIdResult.IsFailed)
            return Result.Fail(articleIdResult.Errors);

        Result removeArticleResult = await articlesWriteRepository.RemoveAsync(articleIdResult.Value, cancellationToken);

        if (removeArticleResult.IsFailed)
            return Result.Fail(removeArticleResult.Errors);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}