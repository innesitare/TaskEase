using Mediator;

namespace TaskEase.Core.Messages.MediatorCommands.BoardTasks;

public sealed class DeleteBoardTaskCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}