import { Injectable } from '@angular/core';
import { BASE_API_URL } from '../../apiUrl';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseApiUrl: string = BASE_API_URL;

  constructor(private http:HttpClient) { }

  registerUser(user:User) : Observable<User>{
    return this.http.post<User>(this.baseApiUrl + 'api/Users/register', user);
  }
}
