import {TaskStatus} from "./task-status.model";

export interface BoardTask {
    id: string;
    userId: string;
    title: string;
    description: string;
    status: TaskStatus;
}
