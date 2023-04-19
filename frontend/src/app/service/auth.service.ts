import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  public register(user: User): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}Auth/register`,
      user
    );
  }

  public login(username: string, password: string): Observable<string> {
    return this.http.post(`${this.apiUrl}Auth/login/${username}/${password}`, null, {
      responseType: 'text',
    });
  }

  public getMe(): Observable<string> {
    return this.http.get(`${this.apiUrl}Auth`, {
      responseType: 'text',
    });
  }
}
