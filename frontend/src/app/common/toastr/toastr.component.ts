import { trigger, state, style, transition, animate, keyframes } from '@angular/animations';
import { Component } from '@angular/core';
import { Toast, ToastPackage, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-toastr',
  templateUrl: './toastr.component.html',
  styleUrls: ['./toastr.component.scss'],
  animations: [
    trigger('flyInOut', [
      state('inactive', style({
        display: 'none',
        opacity: 0
      })),
      transition('inactive => active', animate('400ms ease-out', keyframes([
        style({
          opacity: 0,
        }),
        style({
          opacity: 1,
        })
      ]))),
      transition('active => removed', animate('400ms ease-out', keyframes([
        style({
          opacity: 1,
        }),
        style({
          transform: 'translate3d(10%, 0, 0) skewX(10deg)',
          opacity: 0,
        }),
      ]))),
    ]),
  ],
  preserveWhitespaces: false,
})
export class ToastrComponent extends Toast {
    // used for demo purposes
    undoString = 'undo';

    // constructor is only necessary when not using AoT
    // constructor(
    //   protected toastrService: ToastrService,
    //   public toastPackage: ToastPackage,
    // ) {
    //   super(toastrService, toastPackage);
    // }

    ngOnInit() {
      console.log('toastr');
    }

    action(event: Event) {
      event.stopPropagation();
      this.undoString = 'undid';
      this.toastPackage.triggerAction();
      return false;
    }

}
