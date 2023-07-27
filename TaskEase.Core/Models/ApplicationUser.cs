using Microsoft.AspNetCore.Identity;

namespace TaskEase.Core.Models;

public sealed class ApplicationUser : IdentityUser
{
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Password { get; init; }
}