import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BoardTask } from "../../models/board-tasks/board-task.model";
import { BoardTaskService } from "../../services/board-task.service";
import { UserService } from "../../services/user.service";
import { User } from "../../models/users/user.model";

@Component({
  selector: 'app-board-task-card',
  templateUrl: './board-task-card.component.html',
  styleUrls: ['./board-task-card.component.css']
})
export class BoardTaskCardComponent implements OnInit {
  @Input() task: BoardTask;
  user: User;

  constructor(
      private boardTaskService: BoardTaskService,
      private userService: UserService,
  ) { }

  async ngOnInit() {
    if (this.task.userId !== null && this.task.userId !== undefined) {
      await this.getTaskAssigneeName();
    }
  }

  async getTaskAssigneeName(): Promise<void> {
    this.user = await this.userService.getUserById(this.task.userId);
  }

  async deleteBoardTask(): Promise<void> {
    await this.boardTaskService.deleteBoardTask(this.task.id);
  }
}
