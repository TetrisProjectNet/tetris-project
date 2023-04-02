import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShopItem } from '../model/shop-item';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class ShopService extends BaseService<ShopItem> {
  constructor(
    // public override config: ConfigService,
    public override http: HttpClient
  ) {
    // super(config, http);
    super(http);
    // this.entity = 'shop';
    this.entity = 'ShopItem';
  }
}
