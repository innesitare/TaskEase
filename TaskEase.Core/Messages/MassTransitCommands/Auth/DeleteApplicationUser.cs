namespace TaskEase.Core.Messages.MassTransitCommands.Auth;

public sealed class DeleteApplicationUser
{
    public required Guid Id { get; init; }
}