using Microsoft.Extensions.DependencyInjection;

namespace TaskEase.Core.Extensions;

public static class RedisCacheServiceExtensions
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, string connectionString)
    {
        services.AddStackExchangeRedisCache(x => x.Configuration = connectionString);
        return services;
    }
}