using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskEase.Core.Contracts.Requests.Users;
using TaskEase.Core.Helpers;
using TaskEase.Core.Mapping;
using TaskEase.Core.Services.Abstractions;

namespace TaskEase.UsersApi.Controllers;

[ApiController]
[Authorize(Policy = "Bearer")]
public sealed class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(ApiEndpoints.User.GetAll)] 
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var employees = await _userService.GetAllAsync(cancellationToken);
        var responses = employees.Select(e => e.ToResponse());

        return Ok(responses);
    }

    [HttpGet(ApiEndpoints.User.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var employee = await _userService.GetAsync(id, cancellationToken);

        return employee is not null
            ? Ok(employee.ToResponse())
            : NotFound();
    }

    [HttpGet(ApiEndpoints.User.GetByEmail)]
    public async Task<IActionResult> GetByEmail([FromRoute] string email, CancellationToken cancellationToken)
    {
        var employee = await _userService.GetByEmailAsync(email, cancellationToken);

        return employee is not null
            ? Ok(employee.ToResponse())
            : NotFound();
    }

    [HttpPost(ApiEndpoints.User.Create)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = request.ToUser();
        bool isCreated = await _userService.CreateAsync(user, cancellationToken);

        return isCreated
            ? CreatedAtAction(nameof(Get), new {id = user.Id}, user.ToResponse())
            : BadRequest();
    }

    [HttpPut(ApiEndpoints.User.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = request.ToUser(id);
        var updatedUser = await _userService.UpdateAsync(user, cancellationToken);

        return updatedUser is not null
            ? Ok(updatedUser.ToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.User.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _userService.DeleteAsync(id, cancellationToken);

        return isDeleted
            ? Ok()
            : NotFound();
    }
}