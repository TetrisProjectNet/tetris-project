import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  apiUrl = environment.apiUrl + 'Auth';

  loggedUser$: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);

  isLogged: boolean = false;

  constructor(
    private http: HttpClient,
  ) {
    const token = localStorage.getItem('authToken');
    token ? this.setLoginData() : this.resetLoginData();
  }

  public register(user: User): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}/register`,
      user
    );
  }

  public login(username: string, password: string): Observable<string> {
    return this.http.post(`${this.apiUrl}/login/${username}/${password}`, null, {
      responseType: 'text',
    });
  }

  public refreshToken(id: string): Observable<string> {
    return this.http.post(`${this.apiUrl}/refresh-token${id}`, null, {
      responseType: 'text',
    });
  }

  public getMe(): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}`);
  }

  public resetPassword(email: string, newPassword: string, code: string): Observable<string> {
    return this.http.patch(`${this.apiUrl}/reset-password/${email}/${newPassword}/${code}`, null, {
      responseType: 'text',
    });
  }

  public setLoginData() {
    const token = localStorage.getItem('authToken');
    if (token) {
      this.getMe().subscribe({
        next: (loginObject: User) => {
          this.loggedUser$.next(loginObject);
        }
      })
    }
  }

  public resetLoginData() {
    this.loggedUser$.next(null);
    this.isLogged = false;
  }

}
