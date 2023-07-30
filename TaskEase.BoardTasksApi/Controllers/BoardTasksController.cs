using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskEase.Core.Contracts.Requests.BoardTasks;
using TaskEase.Core.Helpers;
using TaskEase.Core.Mapping;
using TaskEase.Core.Services.Abstractions;

namespace TaskEase.BoardTasksApi.Controllers;

[ApiController]
[Authorize(Policy = "Bearer")]
public sealed class BoardTasksController : ControllerBase
{
    private readonly IBoardTaskService _boardTaskService;

    public BoardTasksController(IBoardTaskService boardTaskService)
    {
        _boardTaskService = boardTaskService;
    }

    [HttpGet(ApiEndpoints.BoardTask.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var boardTasks = await _boardTaskService.GetAllAsync(cancellationToken);
        var responses = boardTasks.Select(s => s.ToResponse());

        return Ok(responses);
    }

    [HttpGet(ApiEndpoints.BoardTask.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var boardTask = await _boardTaskService.GetAsync(id, cancellationToken);

        return boardTask is not null
            ? Ok(boardTask.ToResponse())
            : NotFound();
    }

    [HttpPost(ApiEndpoints.BoardTask.Create)]
    public async Task<IActionResult> Create([FromBody] CreateBoardTaskRequest request,
        CancellationToken cancellationToken)
    {
        var boardTask = request.ToBoardTask();
        bool isCreated = await _boardTaskService.CreateAsync(boardTask, cancellationToken);

        return isCreated
            ? CreatedAtAction(nameof(Get), new {id = boardTask.Id}, boardTask.ToResponse())
            : BadRequest();
    }


    [HttpPut(ApiEndpoints.BoardTask.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromRoute] Guid? userId,
        [FromBody] UpdateBoardTaskRequest request,
        CancellationToken cancellationToken)
    {
        var boardTask = request.ToBoardTask(taskId, userId);
        var updatedBoardTask = await _boardTaskService.UpdateAsync(boardTask, cancellationToken);
    
        return updatedBoardTask is not null
            ? Ok(boardTask.ToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.BoardTask.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _boardTaskService.DeleteAsync(id, cancellationToken);

        return isDeleted
            ? Ok()
            : NotFound();
    }
}