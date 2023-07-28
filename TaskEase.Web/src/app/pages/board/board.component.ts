import {Component, OnInit} from "@angular/core";
import {TaskStatus} from "../../models/board-tasks/task-status.model";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {BoardTask} from "../../models/board-tasks/board-task.model";
import {BoardTaskService} from "../../services/board-task.service";

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit{
  boardTasks: BoardTask[];

  constructor(private authService: AuthService, private router: Router, private boardTaskService: BoardTaskService) {
  }

  async ngOnInit() {
    await this.getAllBoardTasks();
  }

  async performLogout(): Promise<void> {
    await this.authService.logout();
    await this.router.navigate(['/login']);
  }

  boardTasksByStatus(status: TaskStatus): BoardTask[] {
    return this.boardTasks.filter((task) => task.status === status);
  }

  async getAllBoardTasks(): Promise<void> {
    this.boardTasks = await this.boardTaskService.getAllBoardTasks();
  }

  protected readonly TaskStatus = TaskStatus;
}
