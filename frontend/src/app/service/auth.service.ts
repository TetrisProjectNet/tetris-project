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

  constructor(
    private http: HttpClient
  ) {
    const token = localStorage.getItem('authToken');
    if (token) {
      console.log('token is yes');
      // const loginObject = JSON.parse(token);
      // console.log('loginObject: ', loginObject);
      // this.access_token$.next(loginObject.accessToken);
      // this.loggedUser$.next(loginObject.user);

      this.getMe().subscribe({
        next: (loginObject: User) => {
          console.log('loginObject: ', loginObject);
          this.loggedUser$.next(loginObject);
        }
      })
    }

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

  // update(entity: T): Observable<T> {
  //   return this.http.patch<T>(`${this.apiUrl}${this.entity}/${entity.id}`, entity);
  // }

}



// eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY0NDEzODkwOTIyM2UzMTAxMjA2MzgwMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJyZXNldDEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY4MjQ4ODgwNH0.1RwQzB_737WKybQFcu9JLlJ8iGKAo9mQyHbOaB4FBi75gflesDcFP4C2Lm4622US3RvkM6pAf4ZiHznXDm31Yw
// eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY0NDEzODkwOTIyM2UzMTAxMjA2MzgwMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJyZXNldDEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY4MjQ4ODk3NH0.awLSSxQOaszDIPsQ5ZcjTPB_4Faacg9-pdJJfwRlAhXJrDDKt4FhL4xbZRWvNReLbO0CoOBiuPU_nAX8gv8ESw

// {
//   "id": "644138909223e31012063802",
//   "name": "reset1"
// }
// eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY0NDEzODkwOTIyM2UzMTAxMjA2MzgwMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhc2QiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY4MjQ4OTA3N30.fzvO0BY7niZ9qNlccXOxWROhCUQESX5gWNR-CGlRdnguJVQXfRlodGvfBnHJnbQ3hNI_pKWLVDb6QK_BdhnRdg