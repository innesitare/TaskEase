import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {LoginComponent} from "./pages/login/login.component";
import {RegisterComponent} from "./pages/register/register.component";
import {BoardComponent} from "./pages/board/board.component";
import {AuthGuard} from "./guards/auth.guard";
import {TaskCardComponent} from "./pages/task-card/task-card.component";

const routes: Routes =[
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'register', component: RegisterComponent
  },
  {
    path: 'tasks', component: BoardComponent, canActivate: [AuthGuard]
  },
  {
    path: 'task-card', component: TaskCardComponent, canActivate: [AuthGuard]
  }
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
      RouterModule.forRoot(routes)
  ],
  exports: [
      RouterModule
  ]
})
export class AppRoutingModule { }
