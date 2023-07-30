import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { DragDropModule } from "@angular/cdk/drag-drop";
import { MatDialogModule } from "@angular/material/dialog";
import { NgOptimizedImage } from "@angular/common";

import { AppRoutingModule } from './app-routing.module';
import { Router, RouterOutlet } from "@angular/router";

import { AuthService } from "./services/auth.service";
import { BoardTaskService } from "./services/board-task.service";
import { ErrorNotificationService } from "./services/error-notification.service";

import { AppComponent } from './app.component';
import { RegisterComponent } from "./pages/register/register.component";
import { LoginComponent } from "./pages/login/login.component";
import { BoardComponent } from "./pages/board/board.component";
import { BoardTaskCardComponent } from './pages/board-task-card/board-task-card.component';
import { BoardColumnComponent } from './pages/board-column/board-column.component';
import { CreateTaskCardComponent } from './pages/create-task-card/create-task-card.component';

import { DefaultHeadersInterceptor } from "./interceptors/default-headers.interceptor";
import { AuthGuard, authGuardFactory } from "./guards/auth.guard";

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    BoardTaskCardComponent,
    BoardComponent,
    BoardColumnComponent,
    CreateTaskCardComponent
  ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterOutlet,
        HttpClientModule,
        AppRoutingModule,
        NgOptimizedImage,
        DragDropModule,
        MatDialogModule
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
    },
    ErrorNotificationService
  ]
})

export class AppModule {}
