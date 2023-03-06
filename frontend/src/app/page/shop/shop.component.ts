import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faSquarePlus } from '@fortawesome/free-regular-svg-icons';
import { faCirclePlus, faCoins, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { ShopItem } from 'src/app/model/shop-item';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
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
  toastRef: any;

  faCoins = faCoins;
  faTrashCan = faTrashCan;
  faSquarePlus = faSquarePlus;
  faCirclePlus = faCirclePlus;

  constructor(
    private shopService: ShopService,
    private router: Router,
    private toastr: CustomToastrService
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
        .subscribe({
          error: () => this.onDanger('We could delete this shop item.<br>Please try again later!', 'Something went wrong.'),
          complete: () => {
            this.list$ = this.shopService.getAll();
            this.onSuccess('Shop item deleted.');
          }
        });
    }
  }

  onBanOne(id: number): void {
    if (window.confirm('Are you sure about banning this item from the shop?')) {
      this.shopService
        .ban(id)
        .subscribe({
          error: () =>
          this.onDanger('We could not ban this shop item.<br>Please try again later!', 'Something went wrong.'),
          complete: () => {
            this.list$ = this.shopService.getAll();
            this.onSuccess('Shop item banned.');
          }
        });
    }
  }

  onUnbanOne(id: number): void {
    if (window.confirm('Are you sure about unbanning this item?')) {
      this.shopService
        .unban(id)
        .subscribe({
          error: () =>
          this.onDanger('We could not unban this shop item.<br>Please try again later!', 'Something went wrong.'),
          complete: () => {
            this.list$ = this.shopService.getAll();
            this.onSuccess('Shop item unbanned.');
          }
        });
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

  onSuccess(message: string, title: string = 'Success!') {
    this.toastRef = this.toastr.showSuccessToast(title, message);
  }

  onDanger(message: string, title: string = 'Error!') {
    this.toastRef = this.toastr.showDangerToast(title, message);
  }

  onWarning(message: string, title: string = 'Warning!') {
    this.toastRef = this.toastr.showWarningToast(title, message);
  }

  onInfo(message: string, title: string = 'Info!') {
    this.toastRef = this.toastr.showInfoToast(title, message);
  }

  onRemoveToast() {
    this.toastr.removeToast(this.toastRef);
  }


}
