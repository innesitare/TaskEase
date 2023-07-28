using MassTransit;
using Mediator;
using Microsoft.IdentityModel.Tokens;
using TaskEase.Core.Helpers;
using TaskEase.Core.Mapping;
using TaskEase.Core.Messages.MediatorCommands.Auth;

namespace TaskEase.IdentityApi.Pipelines;

public sealed class AuthCommandPipelineBehavior :
    IPipelineBehavior<RegisterUserCommand, string?>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public AuthCommandPipelineBehavior(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async ValueTask<string?> Handle(RegisterUserCommand message, CancellationToken cancellationToken,
        MessageHandlerDelegate<RegisterUserCommand, string?> next)
    {
        string? result = await next(message, cancellationToken);
        if (!result.IsNullOrEmpty())
        {
            var sender = await _sendEndpointProvider.GetSendEndpoint(SendEndpoints.Authentication.Create);
            await sender.Send(message.Request.ToUserMessage(), cancellationToken);
        }

        return result;
    }
}