using Mediator;
using Microsoft.Extensions.Logging;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.Core.Messages.MediatorCommands.BoardTasks.Handlers;

public sealed class CreateBoardTaskCommandHandler : ICommandHandler<CreateBoardTaskCommand, bool>
{
    private readonly IBoardTaskRepository _boardTaskRepository;
    private readonly ILogger<CreateBoardTaskCommandHandler> _logger;

    public CreateBoardTaskCommandHandler(IBoardTaskRepository boardTaskRepository,
        ILogger<CreateBoardTaskCommandHandler> logger)
    {
        _boardTaskRepository = boardTaskRepository;
        _logger = logger;
    }

    public async ValueTask<bool> Handle(CreateBoardTaskCommand command, CancellationToken cancellationToken)
    {
        bool isCreated = await _boardTaskRepository.CreateAsync(command.BoardTask, cancellationToken);
        if (!isCreated)
        {
            _logger.LogError("Failed to create board task.");
        }

        return isCreated;
    }
}