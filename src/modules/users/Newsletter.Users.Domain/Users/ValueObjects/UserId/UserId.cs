using FluentResults;

namespace Newsletter.Users.Domain.Users.ValueObjects.UserId;

public class UserId
{
    private UserId(Guid value) =>
        Value = value;

    public Guid Value { get; }

    public static Result<UserId> New() =>
        new UserId(Guid.NewGuid());

    public static Result<UserId> From(string id)
    {
        try
        {
            return new UserId(Guid.Parse(id));
        }
        catch
        {
            return Result.Fail(UserIdErrors.InvalidUserIdError);
        }
    }

    public static UserId From(Guid id) => new(id);
}