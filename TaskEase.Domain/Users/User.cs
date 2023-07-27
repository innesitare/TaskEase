using TaskEase.Domain.BoardTasks;

namespace TaskEase.Domain.Users;

public sealed class User
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public IEnumerable<BoardTask>? BoardTasks { get; init; }
}