using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Articles.Infrastructure;

namespace Newsletter.Articles.Api;

public static class DiExtensions
{
    public static IServiceCollection AddArticlesApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure(configuration)
            .MapControllers();

        return services;
    }

    private static IServiceCollection MapControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}