using MassTransit;
using TaskEase.Core.Messages.MassTransitCommands.BoardTasks;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.BoardTasksApi.Consumers;

public sealed class DeleteBoardTaskConsumer : IConsumer<DeleteBoardTask>
{
    private readonly IBoardTaskRepository _boardTaskRepository;

    public DeleteBoardTaskConsumer(IBoardTaskRepository boardTaskRepository)
    {
        _boardTaskRepository = boardTaskRepository;
    }

    public async Task Consume(ConsumeContext<DeleteBoardTask> context)
    {
        await _boardTaskRepository.DeleteAsync(context.Message.Id, context.CancellationToken);
    }
}