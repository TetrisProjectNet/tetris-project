import { Pipe, PipeTransform } from '@angular/core';
import { ShopItem } from '../model/shop-item';
import { User } from '../model/user';

@Pipe({
  name: 'shopItemFilter'
})
export class ShopItemFilterPipe implements PipeTransform {

  transform(user: User | null, shopItem: ShopItem): any {
    return user?.shopItems?.some(userShopItem => userShopItem.id === shopItem.id);
  }

}
