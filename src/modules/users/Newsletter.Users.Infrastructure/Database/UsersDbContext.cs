using Microsoft.EntityFrameworkCore;
using Newsletter.Users.Domain.Users;
using Newsletter.Users.Infrastructure.Users;

namespace Newsletter.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : DbContext(dbContextOptions), IUsersDbContext
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}