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
  adminView: boolean = true;

  faCoins = faCoins;
  faTrashCan = faTrashCan;
  faSquarePlus = faSquarePlus;
  faCirclePlus = faCirclePlus;

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
    if (value === 'true') {
      this.onBanOne(id)
    } else {
      this.onUnbanOne(id);
    }
  }

  toggleView(value: any) {
    this.adminView = value;
    console.log(this.adminView);
  }

}
