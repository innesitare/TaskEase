using Mediator;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Notifications.Users;

public sealed class DeleteUserNotification : INotification
{
    public required User? User { get; init; }
}