using Riok.Mapperly.Abstractions;
using TaskEase.Core.Messages.MassTransitCommands.BoardTasks;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.BoardTasksApi.Mapping;

[Mapper]
public static partial class BoardTaskMapper
{
    public static partial BoardTask ToBoardTask(this CreateBoardTask command);
    
    public static partial BoardTask ToBoardTask(this UpdateBoardTask command);
}