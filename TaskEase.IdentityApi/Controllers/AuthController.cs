using Mediator;
using Microsoft.AspNetCore.Mvc;
using TaskEase.Core.Contracts.Requests.Auth;
using TaskEase.Core.Helpers;
using TaskEase.Core.Messages.MediatorCommands.Auth;
using TaskEase.Core.Messages.Queries.Auth;

namespace TaskEase.IdentityApi.Controllers;

[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost(ApiEndpoints.Authentication.Register)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var token = await _sender.Send(new RegisterUserCommand
        {
            Request = request
        }, cancellationToken);

        return token is not null
            ? Ok(token)
            : BadRequest();
    }

    [HttpPost(ApiEndpoints.Authentication.Login)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var token = await _sender.Send(new LoginUserQuery
        {
            Request = request
        }, cancellationToken);

        return token is not null
            ? Ok(token)
            : BadRequest("Login credentials are incorrect");
    }
}