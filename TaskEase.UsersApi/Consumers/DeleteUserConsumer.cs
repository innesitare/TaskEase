using MassTransit;
using TaskEase.Core.Messages.MassTransitCommands.Users;
using TaskEase.Core.Repositories.Abstractions;

namespace TaskEase.UsersApi.Consumers;

public sealed class DeleteUserConsumer : IConsumer<DeleteUser>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<DeleteUser> context)
    {
        await _userRepository.DeleteAsync(context.Message.Id, cancellationToken: context.CancellationToken);
    }
}