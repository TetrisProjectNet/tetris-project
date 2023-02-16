import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import { faCircleChevronDown } from '@fortawesome/free-solid-svg-icons';
import { Observable, switchMap, of } from 'rxjs';
import { ShopItem } from 'src/app/model/shop-item';
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
    if (shopItem.id === 0) {
      this.shopItemService.create(form.value).subscribe(
        () => this.router.navigate(['shop']),
        err => console.error(err)
      );
    } else {
      this.shopItemService.update(shopItem).subscribe(
        () => this.router.navigate(['shop']),
        err => console.error(err)
      );
    }
  }

  getValidationData($event: any) {
    console.log('$event', $event);
  }

}
