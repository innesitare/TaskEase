using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskEase.Core.Models;
using TaskEase.Infrastructure.Persistence.Abstractions;

namespace TaskEase.Infrastructure.Persistence;

public sealed class IdentityDbContext : IdentityDbContext<ApplicationUser>, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }
}