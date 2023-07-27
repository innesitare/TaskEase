using TaskEase.Core.Contracts.Responses.BoardTasks;

namespace TaskEase.Core.Contracts.Responses.Users;

public sealed class UserResponse
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }

    public IEnumerable<BoardTaskResponse>? BoardTasks { get; init; }
}