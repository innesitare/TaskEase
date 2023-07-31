import {TaskStatus} from "../../board-tasks/task-status.model";
import {User} from "../../users/user.model";

export interface UpdateBoardTaskRequest {
    id: string;
    title: string;
    description: string;
    status: TaskStatus;
    user: User | null;
}
