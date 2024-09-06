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

    public static ArticleId New(Guid guid) => new(guid);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}