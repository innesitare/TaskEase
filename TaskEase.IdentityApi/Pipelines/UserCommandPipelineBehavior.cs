using Mediator;
using Microsoft.IdentityModel.Tokens;
using TaskEase.Core.Messages.Commands.Auth;
using TaskEase.Core.Messages.Commands.Users;
using TaskEase.Domain.Users;
using TaskEase.IdentityApi.Mapping;
using TaskEase.IdentityApi.Services.Abstractions;

namespace TaskEase.IdentityApi.Pipelines;

public sealed class UserCommandPipelineBehavior :
    IPipelineBehavior<RegisterUserCommand, string?>,
    IPipelineBehavior<UpdateUserCommand, User?>,
    IPipelineBehavior<DeleteUserCommand, bool>
{
    private readonly IIdentityClientService _client;

    public UserCommandPipelineBehavior(IIdentityClientService client)
    {
        _client = client;
    }

    public async ValueTask<string?> Handle(RegisterUserCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<RegisterUserCommand, string?> next)
    {
        string? result = await next(message, cancellationToken);
        if (!result.IsNullOrEmpty())
        {
            await _client.CreateUser(message.Request.ToGrpcRequest(), cancellationToken);
        }

        return result;
    }

    public async ValueTask<User?> Handle(UpdateUserCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<UpdateUserCommand, User?> next)
    {
        var result = await next(message, cancellationToken);
        if (result is not null)
        {
            await _client.UpdateUser(result.ToGrpcRequest(), cancellationToken);
        }

        return result;
    }

    public async ValueTask<bool> Handle(DeleteUserCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<DeleteUserCommand, bool> next)
    {
        bool result = await next(message, cancellationToken);
        if (result)
        {
            await _client.DeleteUser(message.ToGrpcRequest(), cancellationToken);
        }

        return result;
    }
}