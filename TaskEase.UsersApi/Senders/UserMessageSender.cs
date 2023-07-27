using Grpc.Core;
using TaskEase.Core.Repositories.Abstractions;
using TaskEase.UsersApi.Mapping;

namespace TaskEase.UsersApi.Senders;

public sealed class UserMessageSender : UserService.UserServiceBase
{
    private readonly IUserRepository _userRepository;

    public UserMessageSender(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task<EmptyResponse> CreateUser(CreateUserServiceRequest request, ServerCallContext context)
    {
        var user = request.ToUser();
        await _userRepository.CreateAsync(user, context.CancellationToken);
        
        return new EmptyResponse();
    }

    public override async Task<EmptyResponse> UpdateUser(UpdateUserServiceRequest request, ServerCallContext context)
    {
        var user = request.ToUser();
        await _userRepository.UpdateAsync(user, context.CancellationToken);
        
        return new EmptyResponse();
    }
    
    public override async Task<EmptyResponse> DeleteUser(DeleteUserServiceRequest request, ServerCallContext context)
    {
        await _userRepository.DeleteAsync(request.Id, context.CancellationToken);
        return new EmptyResponse();
    }
}