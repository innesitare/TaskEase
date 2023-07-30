namespace TaskEase.Core.Messages.MassTransitCommands.BoardTasks;

public sealed class UpdateBoardTask
{
    public required string Id { get; init; }
    
    public required string UserId { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public required TaskStatus Status { get; init; } 
}