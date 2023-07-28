using MassTransit;
using Mediator;
using TaskEase.Core.Helpers;
using TaskEase.Core.Messages.MassTransitCommands.Auth;
using TaskEase.Core.Messages.MassTransitCommands.Users;
using TaskEase.Core.Messages.MediatorCommands.Users;
using TaskEase.Domain.Users;

namespace TaskEase.UsersApi.Pipelines;

public sealed class UserCommandPipelineBehavior :
    IPipelineBehavior<UpdateUserCommand, User?>,
    IPipelineBehavior<DeleteUserCommand, bool>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public UserCommandPipelineBehavior(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }
    
    public async ValueTask<User?> Handle(UpdateUserCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<UpdateUserCommand, User?> next)
    {
        var result = await next(message, cancellationToken);
        if (result is not null)
        {
            var sender = await _sendEndpointProvider.GetSendEndpoint(SendEndpoints.User.Update);
            await sender.Send<UpdateUser>(new
            {
                message.User
            }, cancellationToken);
        }
        
        return result;
    }

    public async ValueTask<bool> Handle(DeleteUserCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<DeleteUserCommand, bool> next)
    {
        bool result = await next(message, cancellationToken);
        if (!result)
        {
            var sender = await _sendEndpointProvider.GetSendEndpoint(SendEndpoints.User.Delete);
            await sender.Send(new DeleteApplicationUser
            {
                Id = message.Id
            }, cancellationToken);
        }

        return result;
    }
}