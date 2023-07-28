using Mediator;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.MediatorCommands.BoardTasks;

public sealed class UpdateBoardTaskCommand : ICommand<BoardTask?>
{
    public required BoardTask BoardTask { get; init; }
}