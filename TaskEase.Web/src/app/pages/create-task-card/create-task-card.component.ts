import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { TaskStatus } from "../../models/board-tasks/task-status.model";
import { CreateBoardTaskRequest } from "../../models/requests/board-tasks/create-board-task-request.model";
import { BoardTaskService } from "../../services/board-task.service";

@Component({
  selector: 'app-create-task-card',
  templateUrl: './create-task-card.component.html',
  styleUrls: ['./create-task-card.component.css']
})
export class CreateTaskCardComponent {
  request: CreateBoardTaskRequest;

  constructor(
      @Inject(MAT_DIALOG_DATA) public data: { status: TaskStatus },
      private dialogRef: MatDialogRef<CreateTaskCardComponent>,
      private boardTaskService: BoardTaskService
  ) {
    this.request = {
      title: '',
      description: '',
      status: data.status,
    }
  }

  async createNewBoardTask(): Promise<void> {
    await this.boardTaskService.createBoardTask(this.request);
    this.dialogRef.close();
  }
}
