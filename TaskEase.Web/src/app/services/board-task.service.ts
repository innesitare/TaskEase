import {EventEmitter, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {lastValueFrom, Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import {BoardTask} from "../models/board-tasks/board-task.model";

const apiUrl = 'http://localhost:8080/api/board-tasks';

@Injectable({
  providedIn: 'root'
})
export class BoardTaskService {
  dataDeleter: EventEmitter<void> = new EventEmitter<void>();

  constructor(private http: HttpClient) {
  }

  async getAllBoardTasks(): Promise<BoardTask[]> {
    return await lastValueFrom<BoardTask[]>(this.http.get<BoardTask[]>(apiUrl));
  }

  getBoardTaskById(id: string): Observable<BoardTask> {
    return this.http.get<BoardTask>(`${apiUrl}/${id}`).pipe(
        map(response => response)
    );
  }

  createBoardTask(taskData: BoardTask): Observable<BoardTask> {
    return this.http.post<BoardTask>(apiUrl, taskData).pipe(
        map(response => response)
    );
  }

  updateBoardTask(id: string, taskData: BoardTask): Observable<BoardTask> {
    return this.http.put<BoardTask>(`${apiUrl}/${id}`, taskData).pipe(
        map(response => response)
    );
  }

  async deleteBoardTask(id: string): Promise<any>{
    await lastValueFrom<any>(this.http.delete<any>(`${apiUrl}/${id}`));
    this.dataDeleter.emit();
  }
}