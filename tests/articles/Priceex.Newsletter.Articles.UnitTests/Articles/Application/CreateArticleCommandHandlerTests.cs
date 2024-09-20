using FluentAssertions;
using FluentResults;
using Newsletter.Articles.Application.Articles;
using Newsletter.Articles.Application.Articles.CQRS.Commands.Create;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.Tags;
using Newsletter.Shared.Application.Time;
using NSubstitute;

namespace Priceex.Newsletter.Articles.UnitTests.Articles.Application;

public class CreateArticleCommandHandlerTests
{
    private readonly CreateArticleCommandHandler _sut;

    private readonly IArticlesWriteRepository _articlesWriteRepositoryMock = Substitute.For<IArticlesWriteRepository>();
    private readonly IArticlesUnitOfWork _articlesUnitOfWorkMock = Substitute.For<IArticlesUnitOfWork>();
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    public CreateArticleCommandHandlerTests() =>
        _sut = new CreateArticleCommandHandler(_articlesWriteRepositoryMock, _dateTimeProvider, _articlesUnitOfWorkMock);

    [Fact]
    public async Task Handle_Should_Success()
    {
        // Arrange
        Article article = ArticleHelper.CreateValid();

        CreateArticleCommand createArticleCommand = new(
            Title: article.Title.Value,
            Description: article.Description.Value,
            Content: article.Content.Value,
            Tags: Tag.ListFrom(article.Tags),
            Slug: article.Slug.Value
        );

        Article? createdArticle = null;
        _articlesWriteRepositoryMock
            .When(x => x.AddAsync(Arg.Any<Article>(), Arg.Any<CancellationToken>()))
            .Do(callInfo => createdArticle = callInfo.Arg<Article>());

        // Act
        Result result = await _sut.Handle(createArticleCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        createdArticle.Should().NotBeNull();
        createdArticle?.Title.Value.Should().Be(article.Title.Value);
        createdArticle?.Description.Value.Should().Be(article.Description.Value);
        createdArticle?.Content.Value.Should().Be(article.Content.Value);
        createdArticle?.Tags.Should().BeEquivalentTo(article.Tags);
        createdArticle?.Slug.Value.Should().Be(article.Slug.Value);
    }

    [Fact]
    public async Task Handle_Should_Fail()
    {
        // Arrange
        ArticleData invalidData = ArticleHelper.CreateInvalidData();

        CreateArticleCommand createArticleCommand = new(
            Title: invalidData.Title,
            Description: invalidData.Description,
            Content: invalidData.Content,
            Tags: invalidData.Tags,
            Slug: invalidData.Slug
        );

        // Act
        Result result = await _sut.Handle(createArticleCommand, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }
}