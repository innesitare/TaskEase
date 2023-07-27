using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TaskEase.Core.Models;
using TaskEase.Core.Services.Abstractions;

namespace TaskEase.Core.Services;

internal sealed class AuthService : IAuthService<ApplicationUser>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenWriter<ApplicationUser> _tokenWriter;
    private readonly ILogger _logger;

    public AuthService(SignInManager<ApplicationUser> signInManager, ITokenWriter<ApplicationUser> tokenWriter)
    {
        _signInManager = signInManager;
        _tokenWriter = tokenWriter;
        _logger = signInManager.Logger;
    }

    public async Task<string?> RegisterAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var result = await _signInManager.UserManager.CreateAsync(user, user.Password);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Account cannot be created using the {@Username} username. Result errors: {@Errors}",
                user.UserName, result.Errors);
            return null;
        }

        var registerResult = await _signInManager.PasswordSignInAsync(user.UserName!, user.Password, false, false);
        if (!registerResult.Succeeded)
        {
            _logger.LogWarning(
                "Unexpected error occurred while signing in with password using {@Username} username, IsNotAllowed-{@IsNotAllowed}",
                user.UserName, registerResult.IsNotAllowed.ToString());
            return null;
        }

        var claims = await _signInManager.CreateUserPrincipalAsync(user);
        await _signInManager.UserManager.AddClaimsAsync(user, claims.Claims);

        var token = await _tokenWriter.WriteTokenAsync(user, cancellationToken);
        return token;
    }

    public Task<string?> LoginAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return LoginAsync(user.UserName!, user.Password, cancellationToken);
    }

    public async Task<string?> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
        if (!signInResult.Succeeded)
        {
            _logger.LogWarning(
                "Unexpected error occurred while signing in with password using {@Username} username, IsNotAllowed-{@IsNotAllowed}",
                username, signInResult.IsNotAllowed.ToString());
            return null;
        }

        var applicationUser = await _signInManager.UserManager.FindByNameAsync(username);
        var token = await _tokenWriter.WriteTokenAsync(applicationUser!, cancellationToken);

        return token;
    }
}