﻿using Mediator;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Messages.MediatorCommands.Users;

public sealed class CreateUserCommand : ICommand<bool>
{
    public required User User { get; init; }
}