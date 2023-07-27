using Mediator;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Queries.Users;

public sealed class GetUserByEmailQuery : IQuery<User?>
{
    public required string Email { get; init; }    
}