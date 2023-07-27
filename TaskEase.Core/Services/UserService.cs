using Mediator;
using TaskEase.Core.Helpers;
using TaskEase.Core.Messages.Commands.Users;
using TaskEase.Core.Messages.Queries.Users;
using TaskEase.Core.Services.Abstractions;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Services;

internal sealed class UserService : IUserService
{
    private readonly ISender _sender;
    private readonly ICacheService<User> _cacheService;

    public UserService(ISender sender, ICacheService<User> cacheService)
    {
        _sender = sender;
        _cacheService = cacheService;
    }
    
    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.User.GetAll, async () =>
        {
            var users = await _sender.Send(new GetAllUsersQuery(), cancellationToken);

            return users;
        }, cancellationToken);
    }

    public Task<User?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.User.Get(id.ToString()), async () =>
        {
            var user = await _sender.Send(new GetUserByIdQuery
            {
                Id = id
            }, cancellationToken);

            return user;
        }, cancellationToken);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.User.GetByEmail(email), async () =>
        {
            var user = await _sender.Send(new GetUserByEmailQuery
            {
                Email = email
            }, cancellationToken);

            return user;
        }, cancellationToken);
    }

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        bool isCreated = await _sender.Send(new CreateUserCommand
        {
            User = user
        }, cancellationToken);
        
        return isCreated;
    }

    public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var isUpdated = await _sender.Send(new UpdateUserCommand
        {
            User = user
        }, cancellationToken);
        
        return isUpdated;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteUserCommand
        {
            Id = id
        }, cancellationToken);
        
        return isDeleted;
    }
}