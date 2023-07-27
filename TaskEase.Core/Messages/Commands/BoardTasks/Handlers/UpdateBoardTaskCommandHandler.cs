using Mediator;
using Microsoft.Extensions.Logging;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Commands.BoardTasks.Handlers;

public sealed class UpdateBoardTaskCommandHandler : ICommandHandler<UpdateBoardTaskCommand, BoardTask?>
{
    private readonly IBoardTaskRepository _boardTaskRepository;
    private readonly ILogger<UpdateBoardTaskCommandHandler> _logger;

    public UpdateBoardTaskCommandHandler(IBoardTaskRepository boardTaskRepository,
        ILogger<UpdateBoardTaskCommandHandler> logger)
    {
        _boardTaskRepository = boardTaskRepository;
        _logger = logger;
    }

    public async ValueTask<BoardTask?> Handle(UpdateBoardTaskCommand command, CancellationToken cancellationToken)
    {
        var updatedBoardTask = await _boardTaskRepository.UpdateAsync(command.BoardTask, cancellationToken);
        if (updatedBoardTask is null)
        {
            _logger.LogError("Failed to update board task.");
        }

        return updatedBoardTask;
    }
}