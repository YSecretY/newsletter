using FluentValidation;
using Newsletter.Articles.Domain.Articles.ValueObjects.Contents;
using Newsletter.Articles.Domain.Articles.ValueObjects.Descriptions;
using Newsletter.Articles.Domain.Articles.ValueObjects.Slugs;
using Newsletter.Articles.Domain.Articles.ValueObjects.Tags;
using Newsletter.Articles.Domain.Articles.ValueObjects.Titles;

namespace Newsletter.Articles.Application.Articles.Commands.Create;

internal sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty().MaximumLength(Title.MaxLength);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(Description.MaxLength);
        RuleFor(c => c.Content).NotEmpty().MaximumLength(Content.MaxLength);
        RuleFor(c => c.Slug).NotEmpty().MaximumLength(Slug.MaxLength);
        RuleFor(c => c.Tags)
            .NotEmpty()
            .ForEach(tag => tag.NotEmpty().MaximumLength(Tag.MaxLength))
            .When(c => c.Tags is not null);
    }
}