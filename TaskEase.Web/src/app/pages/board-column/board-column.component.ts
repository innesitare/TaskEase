import {Component, Input, OnInit} from '@angular/core';
import {BoardTask} from "../../models/board-tasks/board-task.model";
import {TaskStatus} from "../../models/board-tasks/task-status.model";
import {BoardTaskService} from "../../services/board-task.service";

@Component({
  selector: 'app-board-column',
  templateUrl: './board-column.component.html',
  styleUrls: ['./board-column.component.css', '../board/board.component.css']
})
export class BoardColumnComponent implements OnInit{
  @Input() title: string;
  @Input() status: TaskStatus;
  @Input() filteredBoardTasks: BoardTask[] = [];

  constructor(private boardTaskService: BoardTaskService) {
  }

  async ngOnInit() {
    this.boardTaskService.dataDeleter.subscribe(async () => {
      let boardingTasks = await this.boardTaskService.getAllBoardTasks();
      this.filteredBoardTasks = boardingTasks.filter(task => task.status === this.status);

      window.location.reload();
    })
  }
}
