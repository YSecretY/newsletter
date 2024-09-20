using Bogus;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;
using Newsletter.Articles.Domain.Articles.ValueObjects.Descriptions;

namespace Priceex.Newsletter.Articles.UnitTests.Articles;

public static class ArticleHelper
{
    public static Article CreateValid()
    {
        Faker faker = new();

        string articleId = ArticleId.NewAsString();
        string title = faker.Lorem.Sentence();
        string description = faker.Lorem.Paragraph();
        string content = faker.Lorem.Paragraphs();
        List<string> tags = ["#sea", "#stories_and_memories", "#friends_with_love"];
        string slug = faker.Lorem.Slug();
        ulong timesReadCount = (ulong)faker.Random.Number(1, 1000_000_000);
        DateTime createdAt = faker.Date.Past();

        return Article.New(
            articleId: articleId,
            title: title,
            description: description,
            content: content,
            tags: tags,
            slug: slug,
            timesReadCount: timesReadCount,
            createdAt: createdAt
        ).Value;
    }

    public static ArticleData CreateInvalidData()
    {
        Faker faker = new();

        return new ArticleData
        {
            Id = Guid.NewGuid().ToString(),
            Title = string.Empty,
            Description = faker.Lorem.Paragraph(Description.MaxLength + 1),
            Content = "\n \t\n ",
            Tags = ["#sea", "#stories_and_memories", "#friends_with_love"],
            Slug = faker.Lorem.Slug(),
            ViewsCount = 5,
            CreatedAt = faker.Date.Future()
        };
    }
}

public class ArticleData
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = null!;

    public string Slug { get; set; } = string.Empty;

    public ulong ViewsCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}