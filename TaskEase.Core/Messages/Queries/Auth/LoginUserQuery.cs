using Mediator;
using TaskEase.Core.Contracts.Requests.Auth;

namespace TaskEase.Core.Messages.Queries.Auth;

public sealed class LoginUserQuery : IQuery<string?>
{
    public required LoginRequest Request { get; init; }
}