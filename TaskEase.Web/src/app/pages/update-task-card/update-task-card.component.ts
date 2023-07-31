import { Component, Inject, OnInit } from '@angular/core';
import { UpdateBoardTaskRequest } from '../../models/requests/board-tasks/update-board-task-request.model';
import { BoardTaskService } from '../../services/board-task.service';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { UserService } from '../../services/user.service';
import { BoardTask } from '../../models/board-tasks/board-task.model';

@Component({
  selector: 'app-update-task-card',
  templateUrl: './update-task-card.component.html',
  styleUrls: ['./update-task-card.component.css'],
})
export class UpdateTaskCardComponent implements OnInit {
  request: UpdateBoardTaskRequest;
  showAssignTaskCard = false;

  constructor(
      @Inject(MAT_DIALOG_DATA) public data: BoardTask,
      private boardTaskService: BoardTaskService,
      private dialog: MatDialog,
      private userService: UserService
  ) {
    this.request = { ...data, user: null };
  }

  async ngOnInit() {
    if (this.data.userId) {
      this.request.user = await this.userService.getUserById(this.data.userId);
    }
  }

  async onApplyClick(): Promise<void> {
    await this.boardTaskService.updateBoardTask(this.request);
    this.dialog.closeAll();
  }

  onToggleAssignTaskCard() {
    this.showAssignTaskCard = !this.showAssignTaskCard;
  }
}
