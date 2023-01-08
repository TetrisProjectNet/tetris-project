import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faSquarePlus } from '@fortawesome/free-regular-svg-icons';
import { faCirclePlus, faCoins, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { ShopItem } from 'src/app/model/shop-item';
import { ShopService } from 'src/app/service/shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  list$ = this.shopService.getAll();
  entity = 'shop';

  faCoins = faCoins;
  faTrashCan = faTrashCan;
  faSquarePlus = faSquarePlus;
  faCirclePlus = faCirclePlus;

  shopItems = [0, 0, 0, 0, 0, 0, 0]

  constructor(
    private shopService: ShopService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onEditOne(shopItem: ShopItem): void {
    this.router.navigate(['/', 'shop', shopItem.id]);
  }

  onDeleteOne(id: number): void {
    if (window.confirm('Are you sure about deleting this item from the shop?')) {
      this.shopService
        .remove(id)
        .subscribe(() => (this.list$ = this.shopService.getAll()));
    }
  }

  onBanOne(id: number): void {
    if (window.confirm('Are you sure about banning this item from the shop?')) {
      this.shopService
        .ban(id)
        .subscribe(() => (this.list$ = this.shopService.getAll()));
    }
  }

  onUnbanOne(id: number): void {
    if (window.confirm('Are you sure about unbanning this item?')) {
      this.shopService
        .unban(id)
        .subscribe(() => (this.list$ = this.shopService.getAll()));
    }
  }

  checkValue(value: any, id: number) {
    console.log(value);
    console.log(id);
    if (value === 'true') {
      this.onBanOne(id)
    } else {
      this.onUnbanOne(id);
    }
    // setTimeout(() => {
    // }, 500);
  }

}
