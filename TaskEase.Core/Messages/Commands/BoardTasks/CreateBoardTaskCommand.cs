using Mediator;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Commands.BoardTasks;

public sealed class CreateBoardTaskCommand : ICommand<bool>
{
    public required BoardTask BoardTask { get; init; }
}