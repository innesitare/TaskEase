import {Component, Input} from '@angular/core';
import {BoardTask} from "../../models/board-tasks/board-task.model";
import {TaskStatus} from "../../models/board-tasks/task-status.model";
import {BoardTaskService} from "../../services/board-task.service";

@Component({
  selector: 'app-board-task',
  templateUrl: './board-task.component.html',
  styleUrls: ['./board-task.component.css']
})
export class BoardTaskComponent implements BoardTask {
  @Input() id: string;
  @Input() title: string;
  @Input() description: string;
  @Input() status: TaskStatus;

  constructor(private boardTaskService: BoardTaskService) {
  }

  async deleteBoardTask(): Promise<void> {
    await this.boardTaskService.deleteBoardTask(this.id);
  }
}
