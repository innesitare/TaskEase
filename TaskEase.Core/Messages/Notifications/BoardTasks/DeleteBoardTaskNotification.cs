using Mediator;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Notifications.BoardTasks;

public sealed class DeleteBoardTaskNotification : INotification
{
    public required BoardTask BoardTask { get; init; }
}