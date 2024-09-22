using FluentResults;

namespace Newsletter.Users.Domain.Users;

public static class UserErrors
{
    public static readonly Error UserCreatedInFutureError = new("User created at time cannot be later than now.");
}