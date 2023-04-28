import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../model/user';

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

  ban(entity: T): Observable<T> {
    return this.http.patch<T>(`${this.apiUrl}${this.entity}/${entity.id}`, entity);
  }

  unban(entity: T): Observable<T> {
    return this.http.patch<T>(`${this.apiUrl}${this.entity}/${entity.id}`, entity);
  }

}
