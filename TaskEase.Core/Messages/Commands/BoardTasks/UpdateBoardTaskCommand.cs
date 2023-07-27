using Mediator;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Commands.BoardTasks;

public sealed class UpdateBoardTaskCommand : ICommand<BoardTask?>
{
    public required BoardTask BoardTask { get; init; }
}