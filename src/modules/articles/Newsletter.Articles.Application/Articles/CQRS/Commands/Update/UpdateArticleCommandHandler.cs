using FluentResults;
using MediatR;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;

namespace Newsletter.Articles.Application.Articles.CQRS.Commands.Update;

public sealed class UpdateArticleCommandHandler(
    IArticlesWriteRepository articlesWriteRepository,
    IArticlesUnitOfWork unitOfWork
) : IRequestHandler<UpdateArticleCommand, Result>
{
    public async Task<Result> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
    {
        Result<Article> articleResult = Article.New(
            articleId: command.Id,
            title: command.CreateArticleCommand.Title,
            description: command.CreateArticleCommand.Description,
            content: command.CreateArticleCommand.Content,
            tags: command.CreateArticleCommand.Tags ?? [],
            slug: command.CreateArticleCommand.Slug,
            timesReadCount: 0,
            createdAt: command.CreatedAt
        );

        if (articleResult.IsFailed)
            return Result.Fail(articleResult.Errors);

        articlesWriteRepository.Update(articleResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}