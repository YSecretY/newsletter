using FluentResults;

namespace Newsletter.Users.Domain.Users.ValueObjects.UserId;

public static class UserIdErrors
{
    public static readonly Error InvalidUserIdError = new("Invalid user id.");
}