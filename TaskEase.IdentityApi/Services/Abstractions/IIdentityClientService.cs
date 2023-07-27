namespace TaskEase.IdentityApi.Services.Abstractions;

public interface IIdentityClientService
{
    Task CreateUser(CreateUserServiceRequest request, CancellationToken cancellationToken);

    Task UpdateUser(UpdateUserServiceRequest request, CancellationToken cancellationToken);

    Task DeleteUser(DeleteUserServiceRequest request, CancellationToken cancellationToken);
}