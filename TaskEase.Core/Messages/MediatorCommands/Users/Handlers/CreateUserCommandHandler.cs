using Mediator;
using Microsoft.Extensions.Logging;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.Core.Messages.MediatorCommands.Users.Handlers;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async ValueTask<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        bool isCreated = await _userRepository.CreateAsync(command.User, cancellationToken);
        if (!isCreated)
        {
            _logger.LogError("Failed to create user.");
        }

        return isCreated;
    }
}