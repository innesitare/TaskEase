using Microsoft.AspNetCore.Mvc;

namespace TaskEase.Core.Contracts.Requests.BoardTasks;

public sealed class UpdateBoardTaskRequest
{
    [FromRoute]
    public Guid Id { get; internal set; }
    
    [FromRoute]
    public Guid UserId { get; internal set; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public required TaskStatus Status { get; init; }
}