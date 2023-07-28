import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'

import { AppComponent } from './app.component'
import {FormsModule} from "@angular/forms";
import {AuthService} from "./services/auth.service";
import {Router, RouterOutlet} from "@angular/router";
import {RegisterComponent} from "./pages/register/register.component";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {LoginComponent} from "./pages/login/login.component";
import {AppRoutingModule} from './app-routing.module';
import {TaskCardComponent} from "./pages/task-card/task-card.component";
import {BoardTaskComponent} from './pages/board-task/board-task.component';
import {BoardComponent} from "./pages/board/board.component";
import {AuthGuard, authGuardFactory} from "./guards/auth.guard";
import { BoardColumnComponent } from './pages/board-column/board-column.component';
import {DefaultHeadersInterceptor} from "./interceptors/default-headers.interceptor";
import {BoardTaskService} from "./services/board-task.service";

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    TaskCardComponent,
    BoardTaskComponent,
    BoardComponent,
    BoardColumnComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterOutlet,
    HttpClientModule,
    AppRoutingModule,
  ],
  bootstrap: [AppComponent],
  providers: [
    AuthService,
    BoardTaskService,
    {
      provide: AuthGuard,
      useFactory: authGuardFactory,
      deps: [AuthService, Router]
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: DefaultHeadersInterceptor,
      multi: true,
    }
  ]
})

export class AppModule {}