using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Articles.Domain.Articles.ValueObjects;

public class ArticleId : ValueObject
{
    public Guid Value { get; }

    private ArticleId(Guid guid)
    {
        Value = guid;
    }

    public static ArticleId New() => new(Guid.NewGuid());

    public static ArticleId From(Guid guid) => new(guid);

    public static ArticleId From(string id) => new(Guid.Parse(id));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}