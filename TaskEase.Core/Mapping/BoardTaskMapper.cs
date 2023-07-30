using Riok.Mapperly.Abstractions;
using TaskEase.Core.Contracts.Requests.BoardTasks;
using TaskEase.Core.Contracts.Responses.BoardTasks;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Core.Mapping;

[Mapper]
public static partial class BoardTaskMapper
{
    public static partial BoardTaskResponse ToResponse(this BoardTask user);
    
    public static partial BoardTask ToBoardTask(this CreateBoardTaskRequest request);
    
    public static partial BoardTask ToBoardTask(this UpdateBoardTaskRequest request);
    
    public static BoardTask ToBoardTask(this UpdateBoardTaskRequest request, Guid taskId, Guid? userId)
    {
        request.Id = taskId;
        request.UserId = userId;
        
        return request.ToBoardTask();
    }   
}