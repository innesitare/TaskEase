using Mediator;
using Microsoft.Extensions.Logging;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Commands.Users.Handlers;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, User?>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(IUserRepository userRepository,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async ValueTask<User?> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var updatedUser = await _userRepository.UpdateAsync(command.User, cancellationToken);
        if (updatedUser is null)
        {
            _logger.LogError("Failed to update board task.");
        }

        return updatedUser;
    }
}