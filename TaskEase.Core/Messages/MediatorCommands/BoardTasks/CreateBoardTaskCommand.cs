using Mediator;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.MediatorCommands.BoardTasks;

public sealed class CreateBoardTaskCommand : ICommand<bool>
{
    public required BoardTask BoardTask { get; init; }
}