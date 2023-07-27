using TaskEase.Domain.Users;

namespace TaskEase.Core.Repositories.Abstractions;

public interface IUserRepository : IRepository<User, string>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}