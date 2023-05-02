import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Email } from '../model/email';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class EmailService extends BaseService<Email> {
  constructor(
    public override http: HttpClient
  ) {
    super(http);
    this.entity = 'Email';
  }
}
