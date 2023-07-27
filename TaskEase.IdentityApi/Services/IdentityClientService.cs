using TaskEase.Core.Services.Abstractions;
using TaskEase.IdentityApi.Services.Abstractions;

namespace TaskEase.IdentityApi.Services;

public sealed class IdentityClientService : IIdentityClientService
{
    private readonly UserService.UserServiceClient _client;
    

    public IdentityClientService(UserService.UserServiceClient client)
    {
        _client = client;
    }
    
    public async Task CreateUser(CreateUserServiceRequest request, CancellationToken cancellationToken)
    {
        await _client.CreateUserAsync(request, cancellationToken: cancellationToken);
    }
    
    public async Task UpdateUser(UpdateUserServiceRequest request, CancellationToken cancellationToken)
    {
        await _client.UpdateUserAsync(request, cancellationToken: cancellationToken);
    }
    
    public async Task DeleteUser(DeleteUserServiceRequest request, CancellationToken cancellationToken)
    {
        await _client.DeleteUserAsync(request, cancellationToken: cancellationToken);
    }
}