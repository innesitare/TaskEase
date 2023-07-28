using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Mediator;
using Microsoft.Extensions.Options;
using TaskEase.Core.Extensions;
using TaskEase.Core.Models;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Core.Services.Abstractions;
using TaskEase.Core.Settings;
using TaskEase.Core.Validation;
using TaskEase.Infrastructure;
using TaskEase.Infrastructure.Extensions;
using TaskEase.Infrastructure.Persistence;
using TaskEase.Infrastructure.Persistence.Abstractions;
using TaskEase.UsersApi.Consumers;
using TaskEase.UsersApi.Pipelines;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault();
builder.Configuration.AddJwtBearer(builder);

builder.Services.AddControllers();
builder.Services.AddMediator();

builder.Services.AddDatabase<IApplicationDbContext, ApplicationDbContext>(builder.Configuration["ApplicationDbContext:ConnectionString"]!);
builder.Services.AddDatabase<IIdentityDbContext, IdentityDbContext>(builder.Configuration["IdentityDbContext:ConnectionString"]!);

builder.Services.AddRedisCache(builder.Configuration["Redis:ConnectionString"]!);

builder.Services.AddApplicationService(typeof(ICacheService<>));

builder.Services.AddApplicationService(typeof(UserCommandPipelineBehavior).Assembly, typeof(IPipelineBehavior<,>));
builder.Services.AddApplicationService(typeof(IInfrastructureAssemblyMark).Assembly, typeof(IRepository<,>));

builder.Services.AddApplicationService<IAuthService<ApplicationUser>>();
builder.Services.AddApplicationService<ITokenWriter<ApplicationUser>>();

builder.Services.AddApplicationService<IUserService>();

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetRequiredSection("Jwt"))
    .ValidateOnStart();

builder.Services.AddOptions<RabbitMqSettings>()
    .Bind(builder.Configuration.GetRequiredSection("RabbitMq"))
    .ValidateOnStart();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<DeleteUserConsumer>();
    
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqSettings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
        cfg.Host(rabbitMqSettings.Host, "/", hostConfigurator =>
        {
            hostConfigurator.Username(rabbitMqSettings.Username);
            hostConfigurator.Password(rabbitMqSettings.Password);
        });
        
        cfg.ConfigureEndpoints(context);   
    });
});

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<IValidationMarker>(ServiceLifetime.Singleton, includeInternalTypes: true);

builder.Services.AddIdentityConfiguration();

var app = builder.Build();

app.MapControllers();

app.Run();