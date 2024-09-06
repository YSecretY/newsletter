using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Articles.Infrastructure;

namespace Newsletter.Articles.Presentation;

public static class DiExtensions
{
    public static IServiceCollection AddArticlesApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
}