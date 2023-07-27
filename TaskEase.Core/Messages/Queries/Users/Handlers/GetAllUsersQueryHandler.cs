using Mediator;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Queries.Users.Handlers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return users;
    }
}