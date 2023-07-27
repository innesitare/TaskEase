using Mediator;
using Microsoft.Extensions.Logging;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.Core.Messages.Commands.BoardTasks.Handlers;

public sealed class DeleteBoardTaskCommandHandler : ICommandHandler<DeleteBoardTaskCommand, bool>
{
    private readonly IBoardTaskRepository _boardTaskRepository;
    private readonly ILogger<DeleteBoardTaskCommandHandler> _logger;

    public DeleteBoardTaskCommandHandler(IBoardTaskRepository boardTaskRepository,
        ILogger<DeleteBoardTaskCommandHandler> logger)
    {
        _boardTaskRepository = boardTaskRepository;
        _logger = logger;
    }
    
    public async ValueTask<bool> Handle(DeleteBoardTaskCommand command, CancellationToken cancellationToken)
    {
        bool isDeleted = await _boardTaskRepository.DeleteAsync(command.Id.ToString(), cancellationToken);
        if (!isDeleted)
        {
            _logger.LogError("Failed to delete board task.");
        }

        return isDeleted;
    }
}