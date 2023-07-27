using Riok.Mapperly.Abstractions;
using TaskEase.Core.Contracts.Requests.Auth;
using TaskEase.Core.Messages.Commands.Users;
using TaskEase.Domain.Users;

namespace TaskEase.IdentityApi.Mapping;

[Mapper]
public static partial class AuthenticationMapper
{
    public static partial CreateUserServiceRequest ToGrpcRequest(this RegisterRequest request);

    public static partial UpdateUserServiceRequest ToGrpcRequest(this User user);

    public static partial DeleteUserServiceRequest ToGrpcRequest(this DeleteUserCommand command);
}