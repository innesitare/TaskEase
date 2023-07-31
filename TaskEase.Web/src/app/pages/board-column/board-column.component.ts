import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

import { TaskStatus } from '../../models/board-tasks/task-status.model';
import { BoardTask } from '../../models/board-tasks/board-task.model';
import { BoardTaskService } from '../../services/board-task.service';
import { UserService } from "../../services/user.service";
import { CreateTaskCardComponent } from '../create-task-card/create-task-card.component';
import { UpdateBoardTaskRequest } from '../../models/requests/board-tasks/update-board-task-request.model';
import { UpdateTaskCardComponent } from '../update-task-card/update-task-card.component';
import { BoardTaskCardComponent } from '../board-task-card/board-task-card.component';

@Component({
  selector: 'app-board-column',
  templateUrl: './board-column.component.html',
  styleUrls: ['./board-column.component.css', '../board/board.component.css']
})
export class BoardColumnComponent implements OnInit {
  @Input() title: string;
  @Input() status: TaskStatus;
  @Input() filteredBoardTasks: BoardTask[] = [];

  @ViewChild(BoardTaskCardComponent, { static: false }) taskCardComponent: BoardTaskCardComponent;

  constructor(
      private userService: UserService,
      private boardTaskService: BoardTaskService,
      private dialog: MatDialog
  ) { }

  async ngOnInit() {
    this.subscribeToDataDeleter();
    await this.getFilteredBoardTasks();
  }

  private async getFilteredBoardTasks(): Promise<void> {
    const allBoardTasks = await this.boardTaskService.getAllBoardTasks();
    this.filteredBoardTasks = allBoardTasks.filter(task => task.status === this.status);
  }

  private subscribeToDataDeleter(): void {
    this.boardTaskService.dataDeleter.subscribe(async () => {
      await this.getFilteredBoardTasks();
    });
  }

  async drop(event: CdkDragDrop<BoardTask[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
          event.previousContainer.data,
          event.container.data,
          event.previousIndex,
          event.currentIndex
      );
    }

    const droppedTask: UpdateBoardTaskRequest = {
      ...event.item.data,
      status: this.status,
      user: event.item.data.userId !== null ? await this.userService.getUserById(event.item.data.userId) : null
    };

    this.mapAndFilterTasks(droppedTask);
    await this.boardTaskService.updateBoardTask(droppedTask);
  }

  onCreateButtonClick(): void {
    if (this.dialog.openDialogs.length) {
      return;
    }

    const dialogRef = this.dialog.open(CreateTaskCardComponent, this.getDialogConfig(this.status, {}));

    dialogRef.afterClosed().subscribe(async () => {
      await this.getFilteredBoardTasks();
    });
  }

  onUpdateButtonClick(task: BoardTask) {
    if (this.dialog.openDialogs.length) {
      return;
    }

    const dialogRef = this.dialog.open(UpdateTaskCardComponent, this.getDialogConfig(task.status, {...task}));

    dialogRef.afterClosed().subscribe(async () => {
      await this.getFilteredBoardTasks();
    });
  }

  private mapAndFilterTasks(updatedTask: UpdateBoardTaskRequest) {
    this.filteredBoardTasks = this.filteredBoardTasks
        .map((task) => (task.id === updatedTask.id ? updatedTask : task))
        .filter((task): task is BoardTask => task.hasOwnProperty('userId'));
  }

  private getDialogConfig(status: TaskStatus, data: any) {
    return {
      maxWidth: '650px',
      position: {
        top: '15%',
        left: '33%'
      },
      data: { ...data, status }
    };
  }
}
