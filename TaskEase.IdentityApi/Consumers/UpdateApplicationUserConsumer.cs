using MassTransit;
using Microsoft.AspNetCore.Identity;
using TaskEase.Core.Messages.MassTransitCommands.Auth;
using TaskEase.Core.Models;

namespace TaskEase.IdentityApi.Consumers;

public sealed class UpdateApplicationUserConsumer : IConsumer<UpdateApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateApplicationUserConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<UpdateApplicationUser> context)
    {
        var user = await _userManager.FindByEmailAsync(context.Message.User?.Email!);
        if (user is not null)
        {
            await _userManager.SetEmailAsync(user, context.Message.User?.Email);
        }
    }
}