import {Component, OnInit} from '@angular/core'
import {AuthService} from "../../services/auth.service";
import {RegisterRequest} from "../../models/requests/register-request.model";
import {Router} from "@angular/router";

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
  }

  constructor(private authService: AuthService, private router: Router) {
  }

  async onRegisterButtonClick(): Promise<void> {
    let token = await this.authService.register(this.registerRequest);
    if (token === null) {
      console.error("Cannot retrieve token from register request!");
      return;
    }

    localStorage.setItem('jwt-token', token)
    await this.router.navigate(['/tasks']);
  }
}
