using Riok.Mapperly.Abstractions;
using TaskEase.Core.Contracts.Requests.Users;
using TaskEase.Core.Contracts.Responses.Users;
using TaskEase.Domain.Users;

namespace TaskEase.Core.Mapping;

[Mapper]
public static partial class UserMapper
{
    public static partial UserResponse ToResponse(this User user);
    
    public static partial User ToUser(this CreateUserRequest request);
    
    public static partial User ToUser(this UpdateUserRequest request);
    
    public static User ToUser(this UpdateUserRequest request, Guid id)
    {
        request.Id = id;
        return request.ToUser();
    }   
}