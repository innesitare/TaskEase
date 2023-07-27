using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaskEase.Core.Attributes;

namespace TaskEase.Core.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationService<TInterface>(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<TInterface>()
            .AddClasses(classes =>
            {
                classes.AssignableTo<TInterface>()
                    .WithoutAttribute<CachingDecorator>();
            })
            .AsImplementedInterfaces()
            .WithLifetime(serviceLifetime));

        return services;
    }

    public static IServiceCollection AddApplicationService(
        this IServiceCollection services,
        Type interfaceType,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(interfaceType)
            .AddClasses(classes =>
            {
                classes.AssignableTo(interfaceType)
                    .WithoutAttribute<CachingDecorator>();
            })
            .AsImplementedInterfaces()
            .WithLifetime(serviceLifetime));

        return services;
    }
    
    public static IServiceCollection AddApplicationService(
        this IServiceCollection services,
        Assembly assembly,
        Type interfaceType,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes =>
            {
                classes.AssignableTo(interfaceType)
                    .WithoutAttribute<CachingDecorator>();
            })
            .AsImplementedInterfaces()
            .WithLifetime(serviceLifetime));

        return services;
    }
}