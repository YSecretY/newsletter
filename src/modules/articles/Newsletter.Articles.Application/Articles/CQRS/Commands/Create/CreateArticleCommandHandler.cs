using FluentResults;
using MediatR;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;
using Newsletter.Shared.Application.Time;

namespace Newsletter.Articles.Application.Articles.CQRS.Commands.Create;

public sealed class CreateArticleCommandHandler(
    IArticlesWriteRepository articlesWriteRepository,
    IDateTimeProvider dateTimeProvider,
    IArticlesUnitOfWork unitOfWork
) : IRequestHandler<CreateArticleCommand, Result>
{
    public async Task<Result> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        Result<Article> articleResult = Article.New(
            articleId: ArticleId.NewAsString(),
            title: command.Title,
            description: command.Description,
            content: command.Content,
            tags: command.Tags ?? [],
            slug: command.Slug,
            timesReadCount: 0,
            createdAt: dateTimeProvider.CurrentTime
        );

        if (articleResult.IsFailed)
            return Result.Fail(articleResult.Errors);

        await articlesWriteRepository.AddAsync(articleResult.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}