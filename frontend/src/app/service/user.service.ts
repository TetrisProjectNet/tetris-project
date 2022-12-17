import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../model/user';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService<User> {

  constructor(
    // public override config: ConfigService,
    public override http: HttpClient
    ) {
    // super(config, http);
    super(http);
    this.entity = 'user';
  }
}
