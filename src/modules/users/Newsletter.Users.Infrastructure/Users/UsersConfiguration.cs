using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsletter.Users.Domain.Users;
using Newsletter.Users.Domain.Users.ValueObjects.Email;
using Newsletter.Users.Domain.Users.ValueObjects.FirstName;
using Newsletter.Users.Domain.Users.ValueObjects.PasswordHash;
using Newsletter.Users.Domain.Users.ValueObjects.UserId;

namespace Newsletter.Users.Infrastructure.Users;

public sealed class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasConversion(
                userId => userId.Value,
                dbId => UserId.From(dbId)
            );

        builder.Property(user => user.Email)
            .HasConversion(
                email => email.Value,
                dbEmail => Email.New(dbEmail).Value
            );

        builder.Property(user => user.PasswordHash)
            .HasConversion(
                passwordHash => passwordHash.Value,
                dbPasswordHash => PasswordHash.New(dbPasswordHash).Value
            )
            .HasMaxLength(PasswordHash.MaxLength);

        builder.Property(user => user.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                dbFirstName => FirstName.New(dbFirstName).Value
            )
            .HasMaxLength(FirstName.MaxLength);

        builder.Property(user => user.CreatedAt);

        builder.HasIndex(user => user.Email).IsUnique();
    }
}