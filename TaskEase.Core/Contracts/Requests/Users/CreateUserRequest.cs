using TaskEase.Core.Contracts.Requests.BoardTasks;

namespace TaskEase.Core.Contracts.Requests.Users;

public sealed class CreateUserRequest
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }

    public IEnumerable<CreateBoardTaskRequest>? BoardTasks { get; init; }
}