using MassTransit;
using TaskEase.BoardTasksApi.Mapping;
using TaskEase.Core.Messages.MassTransitCommands.BoardTasks;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.BoardTasksApi.Consumers;

public sealed class UpdateBoardTaskConsumer : IConsumer<UpdateBoardTask>
{
    private readonly IBoardTaskRepository _boardTaskRepository;

    public UpdateBoardTaskConsumer(IBoardTaskRepository boardTaskRepository)
    {
        _boardTaskRepository = boardTaskRepository;
    }

    public async Task Consume(ConsumeContext<UpdateBoardTask> context)
    {
        await _boardTaskRepository.UpdateAsync(context.Message.ToBoardTask(), context.CancellationToken);
    }
}