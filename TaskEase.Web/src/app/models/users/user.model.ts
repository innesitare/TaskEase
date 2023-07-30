import {BoardTask} from "../board-tasks/board-task.model";

export interface User {
    id: string;
    name: string;
    lastName: string;
    assignedTasks: BoardTask[]
}
