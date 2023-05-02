import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShopItem } from '../model/shop-item';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class ShopService extends BaseService<ShopItem> {
  constructor(
    public override http: HttpClient
  ) {
    super(http);
    this.entity = 'ShopItem';
  }
}
