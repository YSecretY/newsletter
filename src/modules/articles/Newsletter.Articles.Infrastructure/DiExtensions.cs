using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Articles.Application.Articles;
using Newsletter.Articles.Application.Articles.Repositories;
using Newsletter.Articles.Infrastructure.Articles.Persistence;
using Newsletter.Articles.Infrastructure.Database;

namespace Newsletter.Articles.Infrastructure;

public static class DiExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnection = configuration.GetConnectionString("ArticlesDatabase") ?? throw new KeyNotFoundException("Failed to find articles db connection string.");

        services.AddDbContext<IArticlesDbContext, ArticlesDbContext>(options =>
            options.UseNpgsql(dbConnection, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(Schemas.Articles)));

        services.AddScoped<IArticlesUnitOfWork, ArticlesUnitOfWork>();
        services.AddScoped<IArticlesReadRepository, ArticlesReadRepository>();
        services.AddScoped<IArticlesWriteRepository, ArticlesWriteRepository>();
    }
}