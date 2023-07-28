using Mediator;
using TaskEase.Core.Helpers;
using TaskEase.Core.Messages.MediatorCommands.BoardTasks;
using TaskEase.Core.Messages.Queries.BoardTasks;
using TaskEase.Core.Services.Abstractions;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Services;

internal sealed class BoardTaskService : IBoardTaskService
{
    private readonly ISender _sender;
    private readonly ICacheService<BoardTask> _cacheService;

    public BoardTaskService(ISender sender, ICacheService<BoardTask> cacheService)
    {
        _sender = sender;
        _cacheService = cacheService;
    }

    public Task<IEnumerable<BoardTask>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.BoardTask.GetAll, async () =>
        {
            var boardTasks = await _sender.Send(new GetAllBoardTasksQuery(), cancellationToken);

            return boardTasks;
        }, cancellationToken);
    }

    public Task<BoardTask?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.BoardTask.Get(id.ToString()), async () =>
        {
            var boardTask = await _sender.Send(new GetBoardTaskByIdQuery
            {
                Id = id
            }, cancellationToken);

            return boardTask;
        }, cancellationToken);
    }

    public async Task<bool> CreateAsync(BoardTask boardTask, CancellationToken cancellationToken)
    {
        bool isCreated = await _sender.Send(new CreateBoardTaskCommand
        {
            BoardTask = boardTask
        }, cancellationToken);
        
        return isCreated;
    }

    public async Task<BoardTask?> UpdateAsync(BoardTask boardTask, CancellationToken cancellationToken)
    {
        var isUpdated = await _sender.Send(new UpdateBoardTaskCommand
        {
            BoardTask = boardTask
        }, cancellationToken);
        
        return isUpdated;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteBoardTaskCommand
        {
            Id = id
        }, cancellationToken);
        
        return isDeleted;
    }
}