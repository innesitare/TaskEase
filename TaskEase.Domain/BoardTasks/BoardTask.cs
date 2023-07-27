namespace TaskEase.Domain.BoardTasks;

public sealed class BoardTask
{
    public required string Id { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public required TaskStatus Status { get; init; }
}