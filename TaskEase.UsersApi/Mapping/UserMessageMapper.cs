using Riok.Mapperly.Abstractions;
using TaskEase.Domain.Users;

namespace TaskEase.UsersApi.Mapping;

[Mapper]
public static partial class UserMessageMapper
{
    public static partial User ToUser(this CreateUserServiceRequest request);
    
    public static partial User ToUser(this UpdateUserServiceRequest request);
}