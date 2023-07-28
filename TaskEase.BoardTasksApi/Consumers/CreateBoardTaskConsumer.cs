using MassTransit;
using TaskEase.BoardTasksApi.Mapping;
using TaskEase.Core.Messages.MassTransitCommands.BoardTasks;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.BoardTasksApi.Consumers;

public sealed class CreateBoardTaskConsumer : IConsumer<CreateBoardTask>
{
    private readonly IBoardTaskRepository _boardTaskRepository;

    public CreateBoardTaskConsumer(IBoardTaskRepository boardTaskRepository)
    {
        _boardTaskRepository = boardTaskRepository;
    }

    public async Task Consume(ConsumeContext<CreateBoardTask> context)
    {
        await _boardTaskRepository.CreateAsync(context.Message.ToBoardTask(), context.CancellationToken);
    }
}