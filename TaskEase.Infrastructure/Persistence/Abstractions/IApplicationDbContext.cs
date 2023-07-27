using Microsoft.EntityFrameworkCore;
using TaskEase.Domain.BoardTasks;
using TaskEase.Domain.Users;

namespace TaskEase.Infrastructure.Persistence.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; init; }
    
    DbSet<BoardTask> BoardTasks { get; init; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}