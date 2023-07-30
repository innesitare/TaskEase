import { Component } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { ErrorNotificationService } from "../../services/error-notification.service";
import { RegisterRequest } from "../../models/requests/register-request.model";
import { Router } from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: 'register.component.html',
  styleUrls: ['register.component.css']
})
export class RegisterComponent {
  registerRequest: RegisterRequest = {
    name: '',
    lastName: '',
    username: '',
    email: '',
    password: '',
  };

  constructor(
      public errorNotificationService: ErrorNotificationService,
      private authService: AuthService,
      private router: Router
  ) {}

  async onRegisterButtonClick(): Promise<void> {
    try {
      const token = await this.authService.register(this.registerRequest);
      if (!token) {
        this.errorNotificationService.showErrorNotification("Cannot retrieve token from register request!");
        return;
      }

      localStorage.setItem('jwt-token', token);
      await this.router.navigate(['/board']);
    } catch (error) {
      console.error("An error occurred during registration:", error);
      this.errorNotificationService.showErrorNotification("An error occurred during registration. Please try again.");
    }
  }
}
