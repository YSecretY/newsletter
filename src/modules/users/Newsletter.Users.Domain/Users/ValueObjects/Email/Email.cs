using System.Net.Mail;
using FluentResults;
using Newsletter.Shared.Domain.ValueObjects;

namespace Newsletter.Users.Domain.Users.ValueObjects.Email;

public sealed class Email : ValueObject
{
    private Email(string email) =>
        Value = email;

    public string Value { get; }


    public static Result<Email> New(string email)
    {
        if (MailAddress.TryCreate(email, out MailAddress? mailAddress) is false)
            return Result.Fail(EmailErrors.InvalidEmailError);

        return Result.Ok(new Email(mailAddress.Address));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}