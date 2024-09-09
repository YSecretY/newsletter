using FluentValidation;

namespace Newsletter.Articles.Application.Articles.CQRS.Commands.Delete;

internal sealed class DeleteArticleByIdCommandValidator : AbstractValidator<DeleteArticleByIdCommand>
{
    public DeleteArticleByIdCommandValidator()
    {
        RuleFor(c => c.ArticleId).NotNull().NotEmpty();
    }
}