import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { BoardTask } from '../models/board-tasks/board-task.model';
import { CreateBoardTaskRequest } from '../models/requests/board-tasks/create-board-task-request.model';
import { UpdateBoardTaskRequest } from "../models/requests/board-tasks/update-board-task-request.model";

const apiUrl = 'http://localhost:8080/api/board-tasks';

@Injectable({
  providedIn: 'root'
})
export class BoardTaskService {
  dataDeleter = new EventEmitter<void>();

  constructor(private http: HttpClient) {}

  getAllBoardTasks(): Promise<BoardTask[]> {
    return lastValueFrom<BoardTask[]>(this.http.get<BoardTask[]>(apiUrl));
  }

  getBoardTaskById(taskId: string): Promise<BoardTask> {
    return lastValueFrom<BoardTask>(this.http.get<BoardTask>(`${apiUrl}/${taskId}`));
  }

  createBoardTask(request: CreateBoardTaskRequest): Promise<BoardTask> {
    return lastValueFrom<BoardTask>(this.http.post<BoardTask>(apiUrl, request));
  }

  updateBoardTask(request: UpdateBoardTaskRequest): Promise<BoardTask> {
    return lastValueFrom<BoardTask>(this.http.put<BoardTask>(`${apiUrl}/${request.id}/${request.user ? request.user.id : ''}`, request));
  }

  async deleteBoardTask(id: string): Promise<void> {
    await lastValueFrom<void>(this.http.delete<void>(`${apiUrl}/${id}`));
    this.dataDeleter.emit();
  }
}
