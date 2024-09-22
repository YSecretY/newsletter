using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Users.Domain.Users;
using Newsletter.Users.Infrastructure.Database;

namespace Newsletter.Users.Infrastructure;

public static class DiExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnection = configuration.GetConnectionString("UsersDatabase") ?? throw new KeyNotFoundException("Failed to find articles db connection string.");

        services.AddDbContext<IUsersDbContext, UsersDbContext>(options =>
            options.UseNpgsql(dbConnection, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(Schemas.Users)));
    }
}