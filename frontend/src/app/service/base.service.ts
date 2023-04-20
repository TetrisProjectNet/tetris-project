import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService<T extends { id?: string }> {

  apiUrl = environment.apiUrl;
  entity: string = '';

  constructor(
    // public config: ConfigService,
    public http: HttpClient
  ) { }

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(`${this.apiUrl}${this.entity}`);
  }

  get(id: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}${this.entity}/${id}`);
  }

  getBasedOnEmail(email: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}${this.entity}/${email}`);
  }

  isEmailRegistered(email: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}${this.entity}/is-registered/${email}`);
  }

  create(entity: T): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}${this.entity}`, entity);
  }

  update(entity: T): Observable<T> {
    return this.http.patch<T>(`${this.apiUrl}${this.entity}/${entity.id}`, entity);
  }

  remove(id: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}${this.entity}/${id}`);
  }

  ban(id: string): Observable<T> {
    return this.http.patch<T>(`${this.apiUrl}${this.entity}/${id}`, {'banned': true});
  }

  unban(id: string): Observable<T> {
    return this.http.patch<T>(`${this.apiUrl}${this.entity}/${id}`, {'banned': false});
  }

}
