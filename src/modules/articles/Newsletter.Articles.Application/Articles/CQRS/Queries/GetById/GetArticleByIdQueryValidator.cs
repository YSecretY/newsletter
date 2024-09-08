using FluentValidation;

namespace Newsletter.Articles.Application.Articles.CQRS.Queries.GetById;

internal sealed class GetArticleByIdQueryValidator : AbstractValidator<GetArticleByIdQuery>
{
    public GetArticleByIdQueryValidator()
    {
        RuleFor(q => q.ArticleId).NotNull().NotEmpty();
    }
}