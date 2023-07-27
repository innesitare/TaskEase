using Mediator;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Messages.Queries.BoardTasks;

public sealed class GetAllBoardTasksQuery : IQuery<IEnumerable<BoardTask>>
{
}