using Riok.Mapperly.Abstractions;
using TaskEase.Core.Messages.MassTransitCommands.Users;
using TaskEase.Domain.Users;

namespace TaskEase.UsersApi.Mapping;

[Mapper]
public static partial class UserCommandMapper
{
    public static partial User ToUser(this CreateUser command);
    
    public static partial User ToUser(this UpdateUser command);
}