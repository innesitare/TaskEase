using MassTransit;
using TaskEase.Core.Messages.MassTransitCommands.Users;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.UsersApi.Mapping;

namespace TaskEase.UsersApi.Consumers;

public sealed class CreateUserConsumer : IConsumer<CreateUser>
{
    private readonly IUserRepository _userRepository;

    public CreateUserConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<CreateUser> context)
    {
        await _userRepository.CreateAsync(context.Message.ToUser(), cancellationToken: context.CancellationToken);
    }
}