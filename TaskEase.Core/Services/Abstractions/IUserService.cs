using TaskEase.Domain.Users;

namespace TaskEase.Core.Services.Abstractions;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken);
    
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> UpdateAsync(User user, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}