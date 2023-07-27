﻿using Riok.Mapperly.Abstractions;
using TaskEase.Core.Contracts.Requests.Auth;
using TaskEase.Core.Models;

namespace TaskEase.Core.Mapping;

[Mapper]
public static partial class ApplicationUserMapper
{
    public static partial ApplicationUser ToUser(this RegisterRequest request);
}