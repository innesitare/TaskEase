﻿using Mediator;
using TaskEase.Core.Contracts.Requests.Auth;

namespace TaskEase.Core.Messages.MediatorCommands.Auth;

public sealed class RegisterUserCommand : ICommand<string?>
{
    public required RegisterRequest Request { get; init; }
}