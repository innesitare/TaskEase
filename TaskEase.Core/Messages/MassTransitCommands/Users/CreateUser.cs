namespace TaskEase.Core.Messages.MassTransitCommands.Users;

public sealed class CreateUser
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }

    public required string UserName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}