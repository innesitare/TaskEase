namespace TaskEase.Core.Contracts.Requests.BoardTasks;

public sealed class CreateBoardTaskRequest
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public required TaskStatus Status { get; init; } 
}