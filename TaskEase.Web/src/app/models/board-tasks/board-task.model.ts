import {TaskStatus} from "./task-status.model";

export interface BoardTask {
    id: string;
    title: string;
    description: string;
    status: TaskStatus;
}