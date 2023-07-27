﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskEase.Core.Models;
using TaskEase.Core.Services.Abstractions;
using TaskEase.Core.Settings;

namespace TaskEase.Core.Services;

internal sealed class TokenWriter : ITokenWriter<ApplicationUser>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly JwtSettings _jwtSettings;

    public TokenWriter(IServiceScopeFactory scopeFactory, IOptions<JwtSettings> jwtSettings)
    {
        _scopeFactory = scopeFactory;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> WriteTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var claimFactory = _scopeFactory.CreateScope()
            .ServiceProvider
            .GetService<IUserClaimsPrincipalFactory<ApplicationUser>>();

        var claimsPrincipal = await claimFactory!.CreateAsync(user);
        var token = await WriteTokenAsync(claimsPrincipal, cancellationToken);

        return token;
    }

    public Task<string> WriteTokenAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken = default)
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCred = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(_jwtSettings.Issuer,
            _jwtSettings.Audience,
            claimsPrincipal.Claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
            signingCred);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }
}