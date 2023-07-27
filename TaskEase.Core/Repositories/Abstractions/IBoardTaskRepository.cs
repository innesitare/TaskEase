using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Repositories.Abstractions;

public interface IBoardTaskRepository : IRepository<BoardTask, string>
{
}