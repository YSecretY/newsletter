using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.Tags;

namespace Newsletter.Articles.Application.Articles;

public sealed class ArticleDto
{
    private ArticleDto
    (
        string id,
        string title,
        string description,
        string content,
        List<string> tags,
        string slug,
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

    public string Id { get; }

    public string Title { get; }

    public string Description { get; }

    public string Content { get; }

    public List<string> Tags { get; }

    public string Slug { get; }

    public ulong ViewsCount { get; }

    public DateTime CreatedAt { get; }

    public static ArticleDto From(Article article) =>
        new(
            id: article.Id.AsString(),
            title: article.Title.Value,
            description: article.Description.Value,
            content: article.Content.Value,
            tags: Tag.ListFrom(article.Tags),
            slug: article.Slug.Value,
            viewsCount: article.ViewsCount,
            createdAt: article.CreatedAt
        );
}