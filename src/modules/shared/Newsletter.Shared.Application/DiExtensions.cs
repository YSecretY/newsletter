using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Shared.Application.Behaviours.Validation;

namespace Newsletter.Shared.Application;

public static class DiExtensions
{
    public static IServiceCollection AddSharedApplication(this IServiceCollection services, Assembly[] moduleAssemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(moduleAssemblies);

            cfg.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}