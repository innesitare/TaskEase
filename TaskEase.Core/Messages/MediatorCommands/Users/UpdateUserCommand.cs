using Mediator;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.MediatorCommands.Users;

public sealed class UpdateUserCommand : ICommand<User?>
{
    public required User User { get; init; }
}