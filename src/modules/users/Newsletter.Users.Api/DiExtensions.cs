using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Users.Infrastructure;

namespace Newsletter.Users.Api;

public static class DiExtensions
{
    public static IServiceCollection AddUsersApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure(configuration);

        return services;
    }
}