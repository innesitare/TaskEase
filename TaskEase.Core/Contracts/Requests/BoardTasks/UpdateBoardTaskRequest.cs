namespace TaskEase.Core.Contracts.Requests.BoardTasks;

public sealed class UpdateBoardTaskRequest
{
    public Guid Id { get; internal set; }
    
    public Guid? UserId { get; internal set; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public required TaskStatus Status { get; init; }
}
