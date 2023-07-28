using Mediator;
using TaskEase.Core.Mapping;
using TaskEase.Core.Models;
using TaskEase.Core.Services.Abstractions;

namespace TaskEase.Core.Messages.MediatorCommands.Auth.Handlers;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string?>
{
    private readonly IAuthService<ApplicationUser> _authService;
    

    public RegisterUserCommandHandler(IAuthService<ApplicationUser> authService)
    {
        _authService = authService;
    }

    public async ValueTask<string?> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = command.Request.ToUser();
        var token = await _authService.RegisterAsync(user, cancellationToken);
        
        return token;
    }
}