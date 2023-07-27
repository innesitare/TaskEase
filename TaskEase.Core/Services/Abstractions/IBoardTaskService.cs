using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Services.Abstractions;

public interface IBoardTaskService
{
    Task<IEnumerable<BoardTask>> GetAllAsync(CancellationToken cancellationToken);

    Task<BoardTask?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> CreateAsync(BoardTask boardTask, CancellationToken cancellationToken);
    
    Task<BoardTask?> UpdateAsync(BoardTask boardTask, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}