import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Verification } from '../model/verification';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class VerificationService extends BaseService<Verification> {

  constructor(
    // public override config: ConfigService,
    public override http: HttpClient
  ) {
    // super(config, http);
    super(http);
    // this.entity = 'user';
    this.entity = 'Verification';
  }

  // getBasedOnEmail(email: string): Observable<Verification> {
  //   return this.http.get<Verification>(`${this.apiUrl}${this.entity}/${email}`);
  // }

}
