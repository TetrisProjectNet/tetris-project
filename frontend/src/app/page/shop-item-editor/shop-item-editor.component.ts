import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import { faCircleChevronDown } from '@fortawesome/free-solid-svg-icons';
import { Observable, switchMap, of } from 'rxjs';
import { ShopItem } from 'src/app/model/shop-item';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { ShopService } from 'src/app/service/shop.service';

@Component({
  selector: 'app-shop-item-editor',
  templateUrl: './shop-item-editor.component.html',
  styleUrls: ['./shop-item-editor.component.scss']
})
export class ShopItemEditorComponent {

  @ViewChildren('inputRef') inputs!: QueryList<ElementRef>;

  shopItem$: Observable<ShopItem> = this.activatedRoute.params.pipe(
    switchMap(params => {
      if (params['id']) {
        return this.shopItemService.get(params['id'])
      }
      return of(new ShopItem())
    })
  );

  toastRef: any;


  faCircleChevronDown = faCircleChevronDown;
  faSquareCaretDown = faSquareCaretDown;

  clicked: boolean = false;

  titleClass: string = '';
  imageClass: string = '';
  priceClass: string = '';
  colorClass: string = 'focused';
  descriptionClass: string = '';
  selectedElement: any;
  intitialValue: string = '10';

  constructor(
    private activatedRoute: ActivatedRoute,
    private shopItemService: ShopService,
    private router: Router,
    private toastr: CustomToastrService
  ) { }

  ngOnInit(): void {
    // this.titleFocusToggler('clicked');
  }

  ngAfterViewInit() {

    this.inputs.changes.subscribe(sub =>{
      sub.toArray().forEach((element: any) => {
        setTimeout(()=>{

          if (element.nativeElement.value != '' || element.nativeElement.value === '#ffffff') {
            switch(element.nativeElement.name) {
              case 'title': {
                this.titleClass = 'focused';
                break;
              }
              case 'image': {
                this.imageClass = 'focused';
                break;
              }
              case 'price': {
                this.priceClass = 'focused';
                break;
              }
              case 'color': {
                console.log('colored focused');
                this.colorClass = 'focused';
                break;
              }
              case 'description': {
                this.descriptionClass = 'focused';
                break;
              }
            }
          }

        }, 1)
      })
    });

  }

  focusToggler(event: Event, className: string): string {
    event.type == 'focus' ? className= 'focused' : className='';

    if(event) {
      this.selectedElement = event.target;
    } else {
      this.selectedElement = null;
    }

    if (this.selectedElement.value != '') {
      className= 'focused';
    }
    return className;
  }

  titleFocusToggler(event: Event): void {
    this.titleClass = this.focusToggler(event, this.titleClass)
  }

  imageFocusToggler(event: Event): void {
    this.imageClass = this.focusToggler(event, this.imageClass)
  }

  priceFocusToggler(event: Event): void {
    this.priceClass = this.focusToggler(event, this.priceClass)
  }

  colorFocusToggler(event: Event): void {
    this.colorClass = this.focusToggler(event, this.colorClass)
  }

  descriptionFocusToggler(event: Event): void {
    this.descriptionClass = this.focusToggler(event, this.descriptionClass)
  }

  onUpdate(form: NgForm, shopItem: ShopItem): void {
    this.clicked = true;
    if (shopItem.id === '') {
      this.shopItemService.create(shopItem)
      .subscribe({
        error: () => this.onDanger('We could not create the shop item.<br>Please try again later!', 'Something went wrong.'),
        complete: () => {
          this.router.navigate(['shop']);
          this.onSuccess('Shop item created.');
        }
      });
    } else {
      this.shopItemService.update(shopItem)
      .subscribe({
        error: () => this.onDanger('We could not update the shop item.<br>Please try again later!', 'Something went wrong.'),
        complete: () => {
          this.router.navigate(['shop']);
          this.onSuccess('Shop item updated.');
        }
      });
    }
  }

  getValidationData($event: any) {
    console.log('$event', $event);
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
