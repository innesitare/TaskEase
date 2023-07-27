using Mediator;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Queries.Users.Handlers;

public sealed class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetByEmailAsync(query.Email, cancellationToken);
        return users;
    }
}