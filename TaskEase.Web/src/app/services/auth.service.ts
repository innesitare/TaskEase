import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {LoginRequest} from "../models/requests/login-request.model";
import {RegisterRequest} from "../models/requests/register-request.model";
import {lastValueFrom} from "rxjs";

const loginUrl: string = "http://localhost:8080/api/auth/login";
const registerUrl: string = "http://localhost:8080/api/auth/register"

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private httpClient: HttpClient) { }

  async login(loginRequest: LoginRequest): Promise<string> {
    let token = await lastValueFrom(this.httpClient.post(loginUrl, loginRequest, {responseType: "text"}));
    return token !== null ? token : null;
  }

  async register(registerRequest: RegisterRequest): Promise<string> {
    let token = await lastValueFrom(this.httpClient.post(registerUrl, registerRequest, {responseType: "text"}))
    return token !== null ? token : null;
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('jwt-token');
    return token !== null;
  }

  async logout(): Promise<void> {
    localStorage.removeItem('jwt-token');
    return Promise.resolve();
  }
}
