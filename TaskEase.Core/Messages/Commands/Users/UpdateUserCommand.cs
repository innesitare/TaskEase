using Mediator;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Commands.Users;

public sealed class UpdateUserCommand : ICommand<User?>
{
    public required User User { get; init; }
}