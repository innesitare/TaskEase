import {TaskStatus} from "../../board-tasks/task-status.model";

export interface CreateBoardTaskRequest {
    title: string;
    description: string;
    status: TaskStatus;
}
