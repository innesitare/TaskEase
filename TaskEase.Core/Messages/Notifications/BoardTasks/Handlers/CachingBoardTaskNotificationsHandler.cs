using Mediator;
using TaskEase.Core.Extensions;
using TaskEase.Core.Helpers;
using TaskEase.Core.Services.Abstractions;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Notifications.BoardTasks.Handlers;

public sealed class CachingBoardTaskNotificationsHandler :
    INotificationHandler<CreateBoardTaskNotification>,
    INotificationHandler<UpdateBoardTaskNotification>,
    INotificationHandler<DeleteBoardTaskNotification>
{
    private readonly ICacheService<BoardTask> _cacheService;

    public CachingBoardTaskNotificationsHandler(ICacheService<BoardTask> cacheService)
    {
        _cacheService = cacheService;
    }

    public async ValueTask Handle(CreateBoardTaskNotification notification, CancellationToken cancellationToken)
    {
        var boardTask = notification.BoardTask;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.BoardTask.GetAll,
            CacheKeys.BoardTask.Get(boardTask.Id)
        );
    }

    public async ValueTask Handle(UpdateBoardTaskNotification notification, CancellationToken cancellationToken)
    {
        var boardTask = notification.BoardTask;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.BoardTask.GetAll,
            CacheKeys.BoardTask.Get(boardTask.Id)
        );
    }

    public async ValueTask Handle(DeleteBoardTaskNotification notification, CancellationToken cancellationToken)
    {
        var boardTask = notification.BoardTask;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.BoardTask.GetAll,
            CacheKeys.BoardTask.Get(boardTask.Id)
        );
    }
}