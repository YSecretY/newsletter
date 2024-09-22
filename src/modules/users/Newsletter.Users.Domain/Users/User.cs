using FluentResults;
using Newsletter.Users.Domain.Users.ValueObjects.Email;
using Newsletter.Users.Domain.Users.ValueObjects.FirstName;
using Newsletter.Users.Domain.Users.ValueObjects.PasswordHash;
using Newsletter.Users.Domain.Users.ValueObjects.UserId;

namespace Newsletter.Users.Domain.Users;

public sealed class User
{
    private User(UserId id, Email email, PasswordHash passwordHash, FirstName firstName, DateTime createdAt)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        CreatedAt = createdAt;
    }

    public UserId Id { get; }

    public Email Email { get; }

    public PasswordHash PasswordHash { get; }

    public FirstName FirstName { get; }

    public DateTime CreatedAt { get; }

    public static Result<User> New(string userId, string email, string passwordHash, string firstName, DateTime createdAt)
    {
        Result<UserId> userIdResult = UserId.From(userId);
        Result<Email> emailResult = Email.New(email);
        Result<PasswordHash> passwordHashResult = PasswordHash.New(passwordHash);
        Result<FirstName> firstNameResult = FirstName.New(firstName);

        Result validationResult = Result.Merge(
            userIdResult,
            emailResult,
            passwordHashResult,
            firstNameResult,
            Result.FailIf(createdAt > DateTime.UtcNow, UserErrors.UserCreatedInFutureError)
        );

        if (validationResult.IsFailed)
            return Result.Fail(validationResult.Errors);

        return Result.Ok(
            new User(
                id: userIdResult.Value,
                email: emailResult.Value,
                passwordHash: passwordHashResult.Value,
                firstName: firstNameResult.Value,
                createdAt: createdAt
            ));
    }
}