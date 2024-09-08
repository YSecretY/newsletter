using Microsoft.Extensions.DependencyInjection;
using Newsletter.Shared.Application.Time;
using Newsletter.Shared.Infrastructure.Time;

namespace Newsletter.Shared.Infrastructure;

public static class DiExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}