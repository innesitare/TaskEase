using MassTransit;
using Microsoft.AspNetCore.Identity;
using TaskEase.Core.Messages.MassTransitCommands.Auth;
using TaskEase.Core.Models;

namespace TaskEase.IdentityApi.Consumers;

public sealed class DeleteApplicationUserConsumer : IConsumer<DeleteApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteApplicationUserConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<DeleteApplicationUser> context)
    {
        var user = await _userManager.FindByIdAsync(context.Message.Id.ToString());
        if (user is not null)
        {
            await _userManager.DeleteAsync(user);
        }
    }
}