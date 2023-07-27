using Microsoft.EntityFrameworkCore;
using TaskEase.Core.Extensions;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.Domain.Users;
using TaskEase.Infrastructure.Persistence.Abstractions;

namespace TaskEase.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public UserRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        bool isEmpty = await _applicationDbContext.Users.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<User>();
        }
        
        return _applicationDbContext.Users.Include(e => e.BoardTasks);
    }

    public async Task<User?> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.FindAsync(new object?[] {userId}, cancellationToken);
        if (user is null)
        {
            return null;
        }

        await _applicationDbContext.Users.LoadDataAsync(user, e => e.BoardTasks!);
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);

        await _applicationDbContext.Users.LoadDataAsync(user!, e => e.BoardTasks!);
        return user;
    }

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Users.AddAsync(user, cancellationToken);
        int result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0;
    }

    public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        bool isContains = await _applicationDbContext.Users.ContainsAsync(user, cancellationToken);
        if (!isContains)
        {
            return null;
        }

        _applicationDbContext.Users.Update(user);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<bool> DeleteAsync(string userId, CancellationToken cancellationToken)
    {
        int result = await _applicationDbContext.Users
            .Where(e => e.Id == userId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0;
    }
}