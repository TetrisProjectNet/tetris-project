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
    public override http: HttpClient
  ) {
    super(http);
    this.entity = 'Verification';
  }

}
