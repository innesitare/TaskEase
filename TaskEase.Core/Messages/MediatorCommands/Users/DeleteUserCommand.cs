using Mediator;

namespace TaskEase.Core.Messages.MediatorCommands.Users;

public sealed class DeleteUserCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}