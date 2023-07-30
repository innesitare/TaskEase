using Microsoft.EntityFrameworkCore;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.BoardTasks;
using TaskEase.Infrastructure.Persistence.Abstractions;

namespace TaskEase.Infrastructure.Repositories;

public sealed class BoardTaskRepository : IBoardTaskRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public BoardTaskRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<BoardTask>> GetAllAsync(CancellationToken cancellationToken)
    {
        bool isEmpty = await _applicationDbContext.BoardTasks.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<BoardTask>();
        }

        return _applicationDbContext.BoardTasks;
    }

    public async Task<BoardTask?> GetAsync(string boardTaskId, CancellationToken cancellationToken)
    {
        var boardTask = await _applicationDbContext.BoardTasks.FindAsync(new object?[] {boardTaskId}, cancellationToken);

        return boardTask;
    }

    public async Task<bool> CreateAsync(BoardTask boardTask, CancellationToken cancellationToken)
    {
        await _applicationDbContext.BoardTasks.AddAsync(boardTask, cancellationToken);
        int result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<BoardTask?> UpdateAsync(BoardTask boardTask, CancellationToken cancellationToken)
    {
        bool isContains = await _applicationDbContext.BoardTasks.ContainsAsync(boardTask, cancellationToken);
        if (!isContains)
        {
            return null;
        }

        _applicationDbContext.BoardTasks.Update(boardTask);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return boardTask;
    }

    public async Task<bool> DeleteAsync(string boardTaskId, CancellationToken cancellationToken)
    {
        int result = await _applicationDbContext.BoardTasks
            .Where(e => e.Id == boardTaskId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0;
    }
}