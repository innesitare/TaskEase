using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskEase.Domain.BoardTasks;
using TaskEase.Domain.Users;
using TaskEase.Infrastructure.Persistence.Abstractions;

namespace TaskEase.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public required DbSet<User> Users { get; init; }
    
    public required DbSet<BoardTask> BoardTasks { get; init; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}