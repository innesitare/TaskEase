using Mediator;
using Microsoft.Extensions.Logging;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.Core.Messages.Commands.Users.Handlers;

public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<DeleteUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async ValueTask<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        bool isDeleted = await _userRepository.DeleteAsync(command.Id.ToString(), cancellationToken);
        if (!isDeleted)
        {
            _logger.LogError("Failed to delete user.");
        }

        return isDeleted;
    }
}