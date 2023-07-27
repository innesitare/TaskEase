namespace TaskEase.Core.Contracts.Requests.Auth;

public sealed class RegisterRequest
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }

    public required string UserName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}