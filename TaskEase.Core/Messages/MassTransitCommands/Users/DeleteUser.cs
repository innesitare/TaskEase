namespace TaskEase.Core.Messages.MassTransitCommands.Users;

public sealed class DeleteUser
{
    public required string Id { get; init; }
}