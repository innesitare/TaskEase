using TaskEase.Core.Contracts.Requests.BoardTasks;

namespace TaskEase.Core.Contracts.Requests.Users;

public sealed class UpdateUserRequest
{
    public Guid Id { get; internal set; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }

    public IEnumerable<UpdateBoardTaskRequest>? BoardTasks { get; init; }
}