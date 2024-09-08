using FluentValidation;
using Newsletter.Articles.Application.Articles.Commands.Create;

namespace Newsletter.Articles.Application.Articles.Commands.Update;

internal sealed class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleCommandValidator()
    {
        RuleFor(c => c.CreateArticleCommand).SetValidator(new CreateArticleCommandValidator());
    }
}