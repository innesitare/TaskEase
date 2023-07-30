import { Component, OnInit } from "@angular/core";
import { TaskStatus } from "../../models/board-tasks/task-status.model";
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";
import { BoardTask } from "../../models/board-tasks/board-task.model";
import { BoardTaskService } from "../../services/board-task.service";

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {
  readonly TaskStatus = TaskStatus;
  boardTasks: BoardTask[];

  constructor(
      private authService: AuthService,
      private router: Router,
      private boardTaskService: BoardTaskService
  ) {}

  ngOnInit() {
    this.fetchAllBoardTasks();
  }

  async performLogout(): Promise<void> {
    await this.authService.logout();
    await this.router.navigate(['/login']);
  }

  private async fetchAllBoardTasks(): Promise<void> {
    this.boardTasks = await this.boardTaskService.getAllBoardTasks();
  }
}
