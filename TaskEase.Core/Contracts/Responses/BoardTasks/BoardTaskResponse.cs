namespace TaskEase.Core.Contracts.Responses.BoardTasks;

public sealed class BoardTaskResponse
{
    public required string Id { get; init; }
    
    public required string? UserId { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public required TaskStatus Status { get; init; } 
}