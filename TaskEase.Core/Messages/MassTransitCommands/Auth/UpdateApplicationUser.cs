using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.MassTransitCommands.Auth;

public sealed class UpdateApplicationUser
{
    public required User? User { get; init; }
}