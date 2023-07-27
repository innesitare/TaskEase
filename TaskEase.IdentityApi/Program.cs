using FluentValidation;
using FluentValidation.AspNetCore;
using Mediator;
using Serilog;
using TaskEase.Core.Extensions;
using TaskEase.Core.Models;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Core.Services.Abstractions;
using TaskEase.Core.Settings;
using TaskEase.Core.Validation.Auth;
using TaskEase.IdentityApi.Pipelines;
using TaskEase.IdentityApi.Services.Abstractions;
using TaskEase.Infrastructure;
using TaskEase.Infrastructure.Extensions;
using TaskEase.Infrastructure.Persistence;
using TaskEase.Infrastructure.Persistence.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Configuration.AddAzureKeyVault();
builder.Configuration.AddJwtBearer(builder);

builder.Services.AddControllers();
builder.Services.AddMediator();
builder.Services.AddGrpc();

builder.Services.AddDatabase<IApplicationDbContext, ApplicationDbContext>(builder.Configuration["ApplicationDbContext:ConnectionString"]!);
builder.Services.AddDatabase<IIdentityDbContext, IdentityDbContext>(builder.Configuration["IdentityDbContext:ConnectionString"]!);

builder.Services.AddRedisCache(builder.Configuration["Redis:ConnectionString"]!);

builder.Services.AddApplicationService(typeof(ICacheService<>));
builder.Services.AddApplicationService<IIdentityClientService>();

builder.Services.AddApplicationService(typeof(UserCommandPipelineBehavior).Assembly, typeof(IPipelineBehavior<,>));
builder.Services.AddApplicationService(typeof(IInfrastructureAssemblyMark).Assembly, typeof(IRepository<,>));

builder.Services.AddApplicationService<IAuthService<ApplicationUser>>();
builder.Services.AddApplicationService<ITokenWriter<ApplicationUser>>();

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetRequiredSection("Jwt"))
    .ValidateOnStart();

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<LoginRequestValidator>(ServiceLifetime.Singleton, includeInternalTypes: true);

builder.Services.AddGrpcClient<UserService.UserServiceClient>(o => { o.Address = new Uri("https://localhost:5000"); });

builder.Services.AddIdentityConfiguration();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapControllers();
app.MapGrpcService<IIdentityClientService>();

app.Run();