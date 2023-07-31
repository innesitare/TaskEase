import {Component, Input, OnInit, Output} from '@angular/core';
import {User} from "../../models/users/user.model";
import {UserService} from "../../services/user.service";
import {BoardTask} from "../../models/board-tasks/board-task.model";
import {UpdateTaskCardComponent} from "../update-task-card/update-task-card.component";
import {UpdateBoardTaskRequest} from "../../models/requests/board-tasks/update-board-task-request.model";

@Component({
  selector: 'app-assign-task-card',
  templateUrl: './assign-task-card.component.html',
  styleUrls: ['./assign-task-card.component.css']
})

export class AssignTaskCardComponent implements OnInit{
  name: string = ''
  filteredUsers: User[] = [];
  users: User[] = [];

  @Input() task: UpdateBoardTaskRequest;

  constructor(private userService: UserService) { }

  async ngOnInit() {
    this.users = await this.userService.getAllUsers();
    this.filteredUsers = this.users;

      if (this.task.user){
        this.filteredUsers.find(user => user.id === this.task.user.id).isSelected = true;
    }
  }

  onFilterChange(){
    this.filteredUsers = this.users.filter(user => user.name.includes(this.name));
  }

  onUserSelectionChange(selectedUser: User) {
    if (!selectedUser.isSelected) {
      this.task.user = null;
      return;
    }

    this.task.user = selectedUser;
  }
}
