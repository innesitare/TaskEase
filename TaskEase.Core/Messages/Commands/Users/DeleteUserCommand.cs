using Mediator;

namespace TaskEase.Core.Messages.Commands.Users;

public sealed class DeleteUserCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}