import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { User } from '../models/users/user.model';

const apiUrl = 'http://localhost:8080/api/users';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  getAllUsers(): Promise<User[]> {
    return lastValueFrom<User[]>(this.http.get<User[]>(apiUrl));
  }

  getUserById(userId: string): Promise<User> {
    return lastValueFrom<User>(this.http.get<User>(`${apiUrl}/${userId}`));
  }
}
