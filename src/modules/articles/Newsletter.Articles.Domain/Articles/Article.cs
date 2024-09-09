using FluentResults;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;
using Newsletter.Articles.Domain.Articles.ValueObjects.Contents;
using Newsletter.Articles.Domain.Articles.ValueObjects.Descriptions;
using Newsletter.Articles.Domain.Articles.ValueObjects.Slugs;
using Newsletter.Articles.Domain.Articles.ValueObjects.Tags;
using Newsletter.Articles.Domain.Articles.ValueObjects.Titles;

namespace Newsletter.Articles.Domain.Articles;

public sealed class Article
{
    private Article
    (
        ArticleId id,
        Title title,
        Description description,
        Content content,
        List<Tag> tags,
        Slug slug,
        ulong viewsCount,
        DateTime createdAt
    )
    {
        Id = id;
        Title = title;
        Description = description;
        Content = content;
        Tags = tags;
        Slug = slug;
        ViewsCount = viewsCount;
        CreatedAt = createdAt;
    }

    public ArticleId Id { get; }

    public Title Title { get; }

    public Description Description { get; }

    public Content Content { get; }

    public List<Tag> Tags { get; }

    public Slug Slug { get; }

    public ulong ViewsCount { get; }

    public DateTime CreatedAt { get; }

    public static Result<Article> New(
        string articleId,
        string title,
        string description,
        string content,
        List<string> tags,
        string slug,
        ulong timesReadCount,
        DateTime createdAt
    )
    {
        Result<ArticleId> articleIdResult = ArticleId.From(articleId);
        Result<Title> titleResult = Title.New(title);
        Result<Description> descriptionResult = Description.New(description);
        Result<Content> contentResult = Content.New(content);
        Result<List<Tag>> tagsResult = Tag.NewList(tags);
        Result<Slug> slugResult = Slug.New(slug);

        Result validationResult = Result.Merge(
            articleIdResult,
            titleResult,
            descriptionResult,
            contentResult,
            tagsResult,
            slugResult,
            Result.FailIf(createdAt > DateTime.UtcNow, ArticlesErrors.ArticleCreatedInFutureError)
        );

        if (validationResult.IsFailed)
            return Result.Fail(validationResult.Errors);

        return Result.Ok(
            new Article(
                id: articleIdResult.Value,
                title: titleResult.Value,
                description: descriptionResult.Value,
                content: contentResult.Value,
                tags: tagsResult.Value,
                slug: slugResult.Value,
                viewsCount: timesReadCount,
                createdAt: createdAt
            ));
    }
}