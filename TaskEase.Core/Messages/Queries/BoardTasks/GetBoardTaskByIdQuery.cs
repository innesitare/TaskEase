using Mediator;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Queries.BoardTasks;

public sealed class GetBoardTaskByIdQuery : IQuery<BoardTask?>
{
    public required Guid Id { get; init; }
}