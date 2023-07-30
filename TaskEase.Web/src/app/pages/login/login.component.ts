import { Component } from '@angular/core'
import { AuthService } from "../../services/auth.service";
import { ErrorNotificationService } from "../../services/error-notification.service";
import { LoginRequest } from "../../models/requests/login-request.model";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html',
  styleUrls: ['login.component.css']
})
export class LoginComponent {
  loginRequest: LoginRequest = {
    username: '',
    password: ''
  }

  constructor(
      public errorNotificationService: ErrorNotificationService,
      private authService: AuthService,
      private router: Router
  ) {}

  async onLoginButtonClick(): Promise<void> {
    try {
      const token = await this.authService.login(this.loginRequest);
      if (!token) {
        this.errorNotificationService.showErrorNotification("Cannot retrieve token from login request!");
        return;
      }

      localStorage.setItem('jwt-token', token);
      await this.router.navigate(['/board']);
    } catch (error) {
      console.error("An error occurred during login:", error);
      this.errorNotificationService.showErrorNotification("An error occurred during login. Please try again.");
    }
  }
}
