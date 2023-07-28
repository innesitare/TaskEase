using Mediator;
using TaskEase.Core.Messages.MediatorCommands.BoardTasks;
using TaskEase.Core.Messages.Notifications.BoardTasks;
using TaskEase.Core.Messages.Queries.BoardTasks;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.BoardTasksApi.Pipelines;

public sealed class BoardTaskCachingInvalidationPipeline :
    IPipelineBehavior<CreateBoardTaskCommand, bool>,
    IPipelineBehavior<UpdateBoardTaskCommand, BoardTask?>,
    IPipelineBehavior<DeleteBoardTaskCommand, bool>
{
    private readonly IMediator _mediator;

    public BoardTaskCachingInvalidationPipeline(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async ValueTask<bool> Handle(CreateBoardTaskCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<CreateBoardTaskCommand, bool> next)
    {
        bool isCreated = await next(message, cancellationToken);
        if (isCreated)
        {
            await _mediator.Publish(new CreateBoardTaskNotification
            {
                BoardTask = message.BoardTask
            }, cancellationToken);
        }

        return isCreated;
    }

    public async ValueTask<BoardTask?> Handle(UpdateBoardTaskCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<UpdateBoardTaskCommand, BoardTask?> next)
    {
        var boardTask = await next(message, cancellationToken);
        if (boardTask is not null)
        {
            await _mediator.Publish(new UpdateBoardTaskNotification
            {
                BoardTask = message.BoardTask
            }, cancellationToken);
        }

        return boardTask;
    }

    public async ValueTask<bool> Handle(DeleteBoardTaskCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<DeleteBoardTaskCommand, bool> next)
    {
        var boardTask = await _mediator.Send(new GetBoardTaskByIdQuery
        {
            Id = message.Id
        }, cancellationToken);
        
        bool isDeleted = await next(message, cancellationToken);
        if (isDeleted)
        {
            await _mediator.Publish(new DeleteBoardTaskNotification
            {
                BoardTask = boardTask!
            }, cancellationToken);
        }

        return isDeleted;
    }
}