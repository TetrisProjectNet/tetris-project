import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faSquarePlus } from '@fortawesome/free-regular-svg-icons';
import { faCirclePlus, faCoins, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { ShopItem } from 'src/app/model/shop-item';
import { User } from 'src/app/model/user';
import { AuthService } from 'src/app/service/auth.service';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { ShopService } from 'src/app/service/shop.service';
import { UserService } from 'src/app/service/user.service';
import * as AOS from 'aos';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  loggedUser$ = this.authService.loggedUser$;
  isAdmin: boolean = false;
  // loggedUsersShopItems$: Observable<string[]> = this.loggedUser$.subscribe({
  //   next: (user: any) => {
  //     if (user) {
  //       if (!user.shopItems) {
  //         return [];
  //       } else {
  //         let shopItemList: any[] = [];
  //         user.shopItems.map((shopItem: any) => {
  //           shopItemList.push(shopItem.id);
  //         })
  //         console.log('shopitemList: ', shopItemList);
  //         return shopItemList;
  //       }
  //     }
  //     return [];
  //   }
  // })

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
    private toastr: CustomToastrService,
    private authService: AuthService,
    private userService: UserService,

  ) { }

  ngOnInit(): void {
    AOS.init({
      duration: 600,
      offset: 130,
      easing: 'ease-in-sine',
      anchorPlacement: 'bottom-bottom',
    });

    if (this.loggedUser$) {
      this.loggedUser$.subscribe({
        next: (data: any) => {
          if (data?.role == 'admin') {
            this.isAdmin = true;
          }
        }
      })
    }
  }

  onBuyOne(shopItem: ShopItem) {
    if (this.loggedUser$.value) {
      this.loggedUser$.subscribe({
        next: (user: any) => {
          // console.log('hallo');
          if (user) {
            if (user.coins > shopItem.price) {
              if (window.confirm('Are you sure you want to buy this skin?')) {
                user.coins = user.coins - shopItem.price;
                console.log('shopItem ', user.shopItems);
                if (!user.shopItems) {
                  console.log('shop items are null');
                  user.shopItems = [shopItem];
                } else {
                  console.log('shop items are not null');
                  user.shopItems.push(shopItem);
                }
                console.log('shopItem ', user.shopItems);
                this.userService.update(user).subscribe({
                  error: err => {
                    console.error(err);
                    this.onDanger('Purchase failed.<br>Please try again later!');
                  },
                  complete: () => {
                    // this.router.navigate(['user']);
                    this.onSuccess('You\'ve got a new amazing skin.', 'Yeeey!');
                    location.reload()
                  }
                });
              }
            } else {
              this.onWarning('You don\'t have enough coins.');
            }
          }
        }
      })
    } else {
      this.onInfo('Please login to be able to buy this item!', 'Wellcome to the shop!')
      // console.log('this.loggedUser$ is false', this.loggedUser$);
    }

  }

  checkIfPurchased(shopItemId: string) {
    this.loggedUser$.subscribe((user: any) => {
        if (user) {
          if (!user.shopItems) {
            return false;
          } else {
            let shopItemList: any[] = [];
            user.shopItems.map((shopItem: any) => {
              shopItemList.push(shopItem.id);
            })
            // console.log('shopitemList: ', shopItemList);
            // console.log('shopitemId: ', shopItemId);
            console.log('shopitemIncludes: ', shopItemList.includes(shopItemId));
            return shopItemList.includes(shopItemId);
          }
        }
        return false;
      }
    )
  }

  onEditOne(shopItem: ShopItem): void {
    this.router.navigate(['/', 'shop', shopItem.id]);
  }

  onDeleteOne(id: string): void {
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

  onBanOne(shopItem: ShopItem): void {
    if (window.confirm('Are you sure about banning this item from the shop?')) {
      this.shopService
        .ban(shopItem)
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

  onUnbanOne(shopItem: ShopItem): void {
    if (window.confirm('Are you sure about unbanning this item?')) {
      this.shopService
        .unban(shopItem)
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

  checkValue(value: any, shopItem: ShopItem) {
    if (value === 'true') {
      this.onBanOne(shopItem)
    } else {
      this.onUnbanOne(shopItem);
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
