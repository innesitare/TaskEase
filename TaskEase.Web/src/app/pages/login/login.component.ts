import {Component} from '@angular/core'
import {AuthService} from "../../services/auth.service";
import {LoginRequest} from "../../models/requests/login-request.model";
import {Router} from "@angular/router";

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

  constructor(private authService: AuthService, private router: Router) {}

  async onLoginButtonClick(): Promise<void>{
    let token = await this.authService.login(this.loginRequest);
    if (token === null){
      console.error("Cannot retrieve token from login request!");
      return;
    }

    localStorage.setItem('jwt-token', token)
    await this.router.navigate(['/tasks']);
  }

}
