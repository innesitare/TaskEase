using Mediator;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Queries.BoardTasks.Handlers;

public sealed class GetBoardTaskByIdQueryHandler : IQueryHandler<GetBoardTaskByIdQuery, BoardTask?>
{
    private readonly IBoardTaskRepository _boardTaskRepository;

    public GetBoardTaskByIdQueryHandler(IBoardTaskRepository boardTaskRepository)
    {
        _boardTaskRepository = boardTaskRepository;
    }
    
    public async ValueTask<BoardTask?> Handle(GetBoardTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var boardTasks = await _boardTaskRepository.GetAsync(query.Id.ToString(), cancellationToken);
        return boardTasks;
    }
}