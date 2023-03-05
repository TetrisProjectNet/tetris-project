import { trigger, state, style, transition, animate, keyframes } from '@angular/animations';
import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { faCircleCheck, faCircleXmark, } from '@fortawesome/free-regular-svg-icons';
import { faCheck, faCircleExclamation, faCircleInfo, faSquareCheck, faXmark,  faInfo, faExclamation } from '@fortawesome/free-solid-svg-icons';
import { Toast, ToastPackage, ToastrService } from 'ngx-toastr';
import { getAnimations } from '../animations/toastr.animation';

@Component({
  selector: '[app-toastr]',
  templateUrl: './toastr.component.html',
  styleUrls: ['./toastr.component.scss'],
  animations: getAnimations(ToastrComponent.prototype.getAnimationType()),
  // animations: [
  //   trigger('flyInOut', [
  //     state('inactive', style({
  //       opacity: 1,
  //     })),
  //     transition('inactive <=> active', animate('500ms ease-out', keyframes([
  //       style({
  //         transform: 'translateX(340px)',
  //         offset:0,
  //         opacity: 0,
  //       }),
  //       style({
  //         offset:.7,
  //         opacity: 1,
  //         transform: 'translateX(-20px)'
  //       }),
  //       style({
  //         offset: 1,
  //         transform: 'translateX(0)',
  //       })
  //     ]))),
  //     transition('active => removed', animate('500ms ease-in', keyframes([
  //       style({
  //         transform: 'translateX(-20px)',
  //         opacity: 1,
  //         offset: .2
  //       }),
  //       style({
  //         opacity:0,
  //         transform: 'translateX(340px)',
  //         offset: 1
  //       })
  //     ])))
  //   ]),

  //   // trigger('slideInOut', [
  //   //   transition(':enter', [
  //   //     style({ transform: '{{translateType}}(-100%)' }),
  //   //     animate('900ms ease-in', style({ transform: '{{translateType}}(0%)' })),
  //   //   ]),
  //   //   transition(':leave', [animate('700ms ease-in', style({ transform: '{{translateType}}(-100%)' }))]),
  //   // ]),
  // ],
  preserveWhitespaces: false,
})

export class ToastrComponent extends Toast {
  // used for demo purposes
  // undoString = 'undo';

  @ViewChild('toastr') toastr!: ElementRef;

  faSquareCheck = faSquareCheck;
  faCircleCheck = faCircleCheck;
  faCircleXmark = faCircleXmark;
  faCircleInfo = faCircleInfo;
  faInfo = faInfo;
  faCircleExclamation = faCircleExclamation;
  faExclamation = faExclamation;
  faCheck = faCheck;
  faXmark = faXmark;

  screenWidth: number = 0;
  isActive: string = 'inactive';
  isMobile: any;

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
    console.log('matchMedia("(max-width: 600px)").matches', matchMedia("(max-width: 600px)").matches);
    if (matchMedia("(max-width: 600px)").matches) {
      console.log('resize: ', true);
      return true;
    } else {
      console.log('resize: ', false);
      return false;
    }
    // console.log(this.screenWidth);
  }

  ngOnInit(): void {
    // this.screenWidth = window.innerWidth;
    // console.log(this.isMobile);
    if (matchMedia("(max-width: 600px)").matches) {
      this.onResize();
    } else {
      this.onResize();
    }
    // this.isActive = 'active';
  }

  ngAfterViewInit() {
    // console.log(this.toastr);
    // setTimeout(() => {
    //   this.isActive = (this.isActive =='inactive') ? 'active' : 'inactive';
    // })
    // setTimeout(() => {
    //   this.isActive = (this.isActive =='inactive') ? 'active' : 'inactive';
    // }, 500)
  }

  getAnimationType() {
    return this.onResize();
  }

  getAnimationTransformValue(): string {
    // console.log('asdasd');

    return this.screenWidth <= 800 ? 'translateY' : 'translateX';
  }


  // constructor is only necessary when not using AoT
  // constructor(
  //   protected toastrService: ToastrService,
  //   public toastPackage: ToastPackage,
  // ) {
  //   super(toastrService, toastPackage);
  // }

  // action(event: Event) {
  //   event.stopPropagation();
  //   this.undoString = 'undid';
  //   this.toastPackage.triggerAction();
  //   return false;
  // }
}
