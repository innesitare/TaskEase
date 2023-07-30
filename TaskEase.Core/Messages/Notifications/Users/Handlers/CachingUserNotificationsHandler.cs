using Mediator;
using TaskEase.Core.Extensions;
using TaskEase.Core.Helpers;
using TaskEase.Core.Services.Abstractions;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.Notifications.Users.Handlers;

public sealed class CachingUserNotificationsHandler :
    INotificationHandler<CreateUserNotification>,
    INotificationHandler<UpdateUserNotification>,
    INotificationHandler<DeleteUserNotification>
{
    private readonly ICacheService<User> _cacheService;

    public CachingUserNotificationsHandler(ICacheService<User> cacheService)
    {
        _cacheService = cacheService;
    }

    public async ValueTask Handle(CreateUserNotification notification, CancellationToken cancellationToken)
    {
        var user = notification.User;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.User.GetAll,
            CacheKeys.User.Get(user.Id),
            CacheKeys.User.GetByEmail(user.Email)
        );
    }

    public async ValueTask Handle(UpdateUserNotification notification, CancellationToken cancellationToken)
    {
        var user = notification.User;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.User.GetAll,
            CacheKeys.User.Get(user.Id),
            CacheKeys.User.GetByEmail(user.Email)
        );
    }

    public async ValueTask Handle(DeleteUserNotification notification, CancellationToken cancellationToken)
    {
        var user = notification.User;
        var boardTaskIds = user?.BoardTasks!.Select(bt => CacheKeys.BoardTask.Get(bt.Id));

        await _cacheService.RemoveCachesAsync(cancellationToken, boardTaskIds!.ToArray());
        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.BoardTask.GetAll,
            CacheKeys.User.Get(user!.Id),
            CacheKeys.User.GetByEmail(user.Email)
        );
    }
}