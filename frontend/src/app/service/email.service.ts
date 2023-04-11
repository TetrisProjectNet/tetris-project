import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Email } from '../model/email';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class EmailService extends BaseService<Email> {
  constructor(
    // public override config: ConfigService,
    public override http: HttpClient
  ) {
    // super(config, http);
    super(http);
    // this.entity = 'user';
    this.entity = 'Email';
  }
}
