using Mediator;
using TaskEase.Core.Models;
using TaskEase.Core.Services.Abstractions;

namespace TaskEase.Core.Messages.Queries.Auth.Handlers;

public sealed class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, string?>
{
    private readonly IAuthService<ApplicationUser> _authService;

    public LoginUserQueryHandler(IAuthService<ApplicationUser> authService)
    {
        _authService = authService;
    }

    public async ValueTask<string?> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var request = query.Request;
        var token = await _authService.LoginAsync(request.Username, request.Password, cancellationToken);

        return token;
    }
}