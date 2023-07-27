namespace TaskEase.Infrastructure.Persistence.Abstractions;

public interface IIdentityDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}