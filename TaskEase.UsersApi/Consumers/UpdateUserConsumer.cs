using MassTransit;
using TaskEase.Core.Messages.MassTransitCommands.Users;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.UsersApi.Mapping;

namespace TaskEase.UsersApi.Consumers;

public sealed class UpdateUserConsumer : IConsumer<UpdateUser>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<UpdateUser> context)
    {
        await _userRepository.UpdateAsync(context.Message.ToUser(), context.CancellationToken);
    }
}