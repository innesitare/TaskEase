using Mediator;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Queries.BoardTasks.Handlers;

public sealed class GetAllBoardTasksQueryHandler : IQueryHandler<GetAllBoardTasksQuery, IEnumerable<BoardTask>>
{
    private readonly IBoardTaskRepository _boardTaskRepository;

    public GetAllBoardTasksQueryHandler(IBoardTaskRepository boardTaskRepository)
    {
        _boardTaskRepository = boardTaskRepository;
    }

    public async ValueTask<IEnumerable<BoardTask>> Handle(GetAllBoardTasksQuery query, CancellationToken cancellationToken)
    {
        var boardTasks = await _boardTaskRepository.GetAllAsync(cancellationToken);
        return boardTasks;
    }
}