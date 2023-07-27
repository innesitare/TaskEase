using Mediator;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Queries.Users.Handlers;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(query.Id.ToString(), cancellationToken);
        return user;
    }
}