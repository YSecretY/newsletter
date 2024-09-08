using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;

public class ArticleId : ValueObject
{
    public Guid Value { get; }

    private ArticleId(Guid guid)
    {
        Value = guid;
    }

    public static ArticleId New() => new(Guid.NewGuid());

    public static string NewAsString() => Guid.NewGuid().ToString();

    public string AsString() => Value.ToString();

    public static ArticleId From(Guid guid) => new(guid);

    public static Result<ArticleId> From(string id)
    {
        try
        {
            return new ArticleId(Guid.Parse(id));
        }
        catch
        {
            return Result.Fail(ArticleIdErrors.InvalidArticleIdError);
        }
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}