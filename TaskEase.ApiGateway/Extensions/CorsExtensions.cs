namespace TaskEase.ApiGateway.Extensions;

public static class CorsExtensions
{
    public static IConfigurationBuilder AddCors(this IConfigurationBuilder configuration,
        WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:8080")
                    // .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                    // .SetPreflightMaxAge(TimeSpan.FromSeconds(86400))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });
        });
        
        return builder.Configuration;
    }
}