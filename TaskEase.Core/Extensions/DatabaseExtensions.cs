using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using TaskEase.Core.Settings;

namespace TaskEase.Core.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddNpgsqlDbContextOptions<TDbContext>(this IServiceCollection services,
        string connectionString)
        where TDbContext : DbContext
    {
        services.AddTransient<IOptions<PostgresDbContextSettings<TDbContext>>>(_ =>
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            return Options.Create(new PostgresDbContextSettings<TDbContext>
            {
                ConnectionString = connectionString,
                Database = builder.Database!,
                Host = builder.Host!,
                Port = builder.Port
            });
        });

        return services;
    }

    public static IServiceCollection AddDatabase<TDbInterface, TDbContext>(this IServiceCollection services,
        string connectionString)
        where TDbContext : DbContext, TDbInterface
        where TDbInterface : class
    {
        services.AddNpgsql<TDbContext>(connectionString);
        services.AddNpgsqlDbContextOptions<TDbContext>(connectionString);

        services.AddApplicationService<TDbInterface>(services.Single(x =>
        {
            return x.ImplementationType == typeof(TDbContext);
        }).Lifetime);
        return services;
    }
}