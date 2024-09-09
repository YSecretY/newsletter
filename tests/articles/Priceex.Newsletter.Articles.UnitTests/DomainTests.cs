using Bogus;
using FluentAssertions;
using FluentResults;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;
using Newsletter.Articles.Domain.Articles.ValueObjects.Contents;
using Newsletter.Articles.Domain.Articles.ValueObjects.Descriptions;
using Newsletter.Articles.Domain.Articles.ValueObjects.Slugs;
using Newsletter.Articles.Domain.Articles.ValueObjects.Tags;
using Newsletter.Articles.Domain.Articles.ValueObjects.Titles;

namespace Priceex.Newsletter.Articles.UnitTests;

public class DomainTests
{
    [Fact]
    public void Create_Valid_Article_Should_Success()
    {
        // Arrange
        Faker faker = new();

        string articleId = ArticleId.NewAsString();
        string title = faker.Lorem.Sentence();
        string description = faker.Lorem.Paragraph();
        string content = faker.Lorem.Paragraphs(3);
        List<string> tags = ["#sea", "#stories_and_memories", "#friends_with_love"];
        string slug = faker.Lorem.Slug(3);
        ulong timesReadCount = (ulong)faker.Random.Number(1, 1000_000_000);
        DateTime createdAt = faker.Date.Past();

        // Act
        Result<Article> articleCreationResult = Article.New(
            articleId: articleId,
            title: title,
            description: description,
            content: content,
            tags: tags,
            slug: slug,
            timesReadCount: timesReadCount,
            createdAt: createdAt
        );

        // Assert
        articleCreationResult.IsSuccess.Should().BeTrue();

        Article createdArticle = articleCreationResult.Value;

        createdArticle.Id.Should().Be(articleId);
        createdArticle.Title.Value.Should().Be(title);
        createdArticle.Description.Value.Should().Be(description);
        createdArticle.Content.Value.Should().Be(content);
        createdArticle.Tags.Should().BeEquivalentTo(tags.Select(tag => Tag.New(tag).Value).ToList());
        createdArticle.Slug.Value.Should().Be(slug);
        createdArticle.ViewsCount.Should().Be(timesReadCount);
        createdArticle.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Create_TooLong_Title_Should_Return_TooLongTitleError()
    {
        // Arrange
        string title = new Faker().Lorem.Sentence(Title.MaxLength + 1);

        // Act
        Result<Title> titleCreationResult = Title.New(title);

        // Assert
        titleCreationResult.IsFailed.Should().BeTrue();
        titleCreationResult.Errors.Should().Contain(TitleErrors.TitleTooLongError);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("\t")]
    [InlineData("\n    \t   \n")]
    public void Create_Empty_Title_Should_Return_EmptyTitleError(string title)
    {
        // Act
        Result<Title> titleCreationResult = Title.New(title);

        // Assert
        titleCreationResult.IsFailed.Should().BeTrue();
        titleCreationResult.Errors.Should().Contain(TitleErrors.TitleEmptyError);
    }

    [Fact]
    public void Create_TooLong_Description_Should_Return_TooLongDescriptionError()
    {
        // Arrange
        string description = new Faker().Lorem.Sentence(Description.MaxLength + 1);

        // Act
        Result<Description> descriptionResult = Description.New(description);

        // Assert
        descriptionResult.IsFailed.Should().BeTrue();
        descriptionResult.Errors.Should().Contain(DescriptionErrors.DescriptionTooLongError);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("\t")]
    [InlineData("\n    \t   \n")]
    public void Create_Empty_Description_Should_Return_EmptyDescriptionError(string description)
    {
        // Act
        Result<Description> descriptionResult = Description.New(description);

        // Assert
        descriptionResult.IsFailed.Should().BeTrue();
        descriptionResult.Errors.Should().Contain(DescriptionErrors.DescriptionEmptyError);
    }

    [Fact]
    public void Create_TooLong_Content_Should_Return_TooLongContentError()
    {
        // Arrange
        string content = new Faker().Lorem.Sentence(Content.MaxLength + 1);

        // Act
        Result<Content> contentResult = Content.New(content);

        // Assert
        contentResult.IsFailed.Should().BeTrue();
        contentResult.Errors.Should().Contain(ContentErrors.ContentTooLongError);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("\t")]
    [InlineData("\n    \t   \n")]
    public void Create_Empty_Content_Should_Return_EmptyContentError(string content)
    {
        // Act
        Result<Content> contentResult = Content.New(content);

        // Assert
        contentResult.IsFailed.Should().BeTrue();
        contentResult.Errors.Should().Contain(ContentErrors.ContentEmptyError);
    }

    [Fact]
    public void Create_TooLong_Tag_Should_Return_TooLongTagError()
    {
        // Arrange
        string tag = new Faker().Random.String(Tag.MaxLength + 1);

        // Act
        Result<Tag> tagResult = Tag.New(tag);

        // Assert
        tagResult.IsFailed.Should().BeTrue();
        tagResult.Errors.Should().Contain(TagErrors.TagTooLongError);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("\t")]
    [InlineData("\n    \t   \n")]
    public void Create_Empty_Tag_Should_Return_EmptyTagError(string tag)
    {
        // Act
        Result<Tag> tagResult = Tag.New(tag);

        // Assert
        tagResult.IsFailed.Should().BeTrue();
        tagResult.Errors.Should().Contain(TagErrors.TagEmptyError);
    }

    [Theory]
    [InlineData("one two")]
    [InlineData("one\ntwo")]
    [InlineData("one!two")]
    [InlineData("one.two")]
    [InlineData("ont\ttwo")]
    public void Create_Invalid_Tag_Should_Return_TagContainsInvalidSymbolsError(string tag)
    {
        // Act
        Result<Tag> tagResult = Tag.New(tag);

        // Assert
        tagResult.IsFailed.Should().BeTrue();
        tagResult.Errors.Should().Contain(TagErrors.TagContainsInvalidSymbolsError);
    }

    [Fact]
    public void Create_TooLong_Slug_Should_Return_TooLongSlugError()
    {
        // Arrange
        string slug = new Faker().Lorem.Slug(Slug.MaxLength + 1);

        // Act
        Result<Slug> slugResult = Slug.New(slug);

        // Assert
        slugResult.IsFailed.Should().BeTrue();
        slugResult.Errors.Should().Contain(SlugErrors.SlugTooLongError);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("\t")]
    [InlineData("\n    \t   \n")]
    public void Create_Empty_Slug_Should_Return_EmptySlugError(string slug)
    {
        // Act
        Result<Slug> slugResult = Slug.New(slug);

        // Assert
        slugResult.IsFailed.Should().BeTrue();
        slugResult.Errors.Should().Contain(SlugErrors.SlugEmptyError);
    }

    [Theory]
    [InlineData("one two")]
    [InlineData("one\ntwo")]
    [InlineData("one!two")]
    [InlineData("one.two")]
    [InlineData("ont\ttwo")]
    [InlineData("ont_two")]
    public void Create_Invalid_Slug_Should_Return_SlugContainsInvalidSymbolsError(string slug)
    {
        // Act
        Result<Slug> slugResult = Slug.New(slug);

        // Assert
        slugResult.IsFailed.Should().BeTrue();
        slugResult.Errors.Should().Contain(SlugErrors.SlugContainsInvalidSymbolsError);
    }

    [Fact]
    public void Create_Article_In_Future_Should_Return_ArticleCannotBeCreatedAtInFutureError()
    {
        // Arrange
        Faker faker = new();

        string articleId = ArticleId.NewAsString();
        string title = faker.Lorem.Sentence();
        string description = faker.Lorem.Paragraph();
        string content = faker.Lorem.Paragraphs(3);
        List<string> tags = ["#sea", "#stories_and_memories", "#friends_with_love"];
        string slug = faker.Lorem.Slug(3);
        ulong timesReadCount = (ulong)faker.Random.Number(1, 1000_000_000);
        DateTime createdAt = faker.Date.Future();

        // Act
        Result<Article> articleCreationResult = Article.New(
            articleId: articleId,
            title: title,
            description: description,
            content: content,
            tags: tags,
            slug: slug,
            timesReadCount: timesReadCount,
            createdAt: createdAt
        );

        // Assert
        articleCreationResult.IsFailed.Should().BeTrue();
        articleCreationResult.Errors.Should().Contain(ArticlesErrors.ArticleCreatedInFutureError);
    }
}