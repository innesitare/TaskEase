using Mediator;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Queries.Users;

public sealed class GetAllUsersQuery : IQuery<IEnumerable<User>>
{
}