using Mediator;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Queries.Users;

public sealed class GetUserByIdQuery : IQuery<User?>
{
    public required Guid Id { get; init; }
}