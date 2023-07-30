import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { TaskStatus } from '../../models/board-tasks/task-status.model';
import { BoardTask } from '../../models/board-tasks/board-task.model';
import { BoardTaskService } from '../../services/board-task.service';
import { CreateTaskCardComponent } from '../create-task-card/create-task-card.component';

@Component({
  selector: 'app-board-column',
  templateUrl: './board-column.component.html',
  styleUrls: ['./board-column.component.css', '../board/board.component.css']
})
export class BoardColumnComponent implements OnInit {
  @Input() title: string;
  @Input() status: TaskStatus;
  @Input() filteredBoardTasks: BoardTask[] = [];

  constructor(
      private boardTaskService: BoardTaskService,
      private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.getFilteredBoardTasks();
    this.subscribeToDataDeleter();
  }

  private async getFilteredBoardTasks(): Promise<void> {
    const allBoardTasks = await this.boardTaskService.getAllBoardTasks();
    this.filteredBoardTasks = allBoardTasks.filter(task => task.status === this.status);
  }

  private subscribeToDataDeleter(): void {
    this.boardTaskService.dataDeleter.subscribe(async () => {
      await this.getFilteredBoardTasks();
      window.location.reload();
    });
  }

  async drop(event: CdkDragDrop<BoardTask[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem (
          event.previousContainer.data,
          event.container.data,
          event.previousIndex,
          event.currentIndex
      );

      const droppedTask: BoardTask = event.item.data;
      droppedTask.status = this.status;

      const indexToReplace = this.filteredBoardTasks.findIndex(task => task.id === droppedTask.id);
      this.filteredBoardTasks[indexToReplace] = droppedTask;

      await this.boardTaskService.updateBoardTask(droppedTask.id, droppedTask);
    }
  }

  onCreateButtonClick(): void {
    const dialogRef = this.dialog.open(CreateTaskCardComponent, {
      maxWidth: '650px',
      position: {
        top: "15%",
        left: "33%"
      },
      data: {
        status: this.status
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getFilteredBoardTasks();
    });
  }
}
