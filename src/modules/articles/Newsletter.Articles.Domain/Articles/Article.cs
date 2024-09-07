using FluentResults;
using Newsletter.Articles.Domain.Articles.ValueObjects;
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

    public static Result<Article> New(
        ArticleId articleId,
        string title,
        string description,
        string content,
        List<string> tags,
        string slug,
        ulong timesReadCount,
        DateTime createdAt
    )
    {
        Result<Title> titleResult = Title.New(title);
        if (titleResult.IsFailed)
            return Result.Fail(titleResult.Errors);

        Result<Description> descriptionResult = Description.New(description);
        if (descriptionResult.IsFailed)
            return Result.Fail(descriptionResult.Errors);

        Result<Content> contentResult = Content.New(content);
        if (contentResult.IsFailed)
            return Result.Fail(contentResult.Errors);

        List<Tag> createdTags = new(tags.Count);
        foreach (string tag in tags)
        {
            Result<Tag> tagResult = Tag.New(tag);
            if (tagResult.IsFailed)
                return Result.Fail(tag);

            createdTags.Add(tagResult.Value);
        }

        Result<Slug> slugResult = Slug.New(slug);
        if (slugResult.IsFailed)
            return Result.Fail(slug);

        if (createdAt > DateTime.UtcNow)
            return Result.Fail(ArticlesErrors.ArticleCreatedInFutureError);

        return Result.Ok(new Article(
            id: articleId,
            title: titleResult.Value,
            description: descriptionResult.Value,
            content: contentResult.Value,
            tags: createdTags,
            slug: slugResult.Value,
            viewsCount: timesReadCount,
            createdAt: createdAt
        ));
    }

    public ArticleId Id { get; }

    public Title Title { get; }

    public Description Description { get; }

    public Content Content { get; }

    public List<Tag> Tags { get; }

    public Slug Slug { get; }

    public ulong ViewsCount { get; }

    public DateTime CreatedAt { get; }
}