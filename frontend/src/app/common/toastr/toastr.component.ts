import { trigger, state, style, transition, animate, keyframes } from '@angular/animations';
import { Component } from '@angular/core';
import { faCheck, faSquareCheck } from '@fortawesome/free-solid-svg-icons';
import { Toast, ToastPackage, ToastrService } from 'ngx-toastr';

@Component({
  selector: '[app-toastr]',
  templateUrl: './toastr.component.html',
  styleUrls: ['./toastr.component.scss'],
  animations: [
    trigger('flyInOut', [
      state('inactive', style({
        opacity: 1,
      })),
      transition('inactive <=> active', animate('500ms ease-out', keyframes([
        style({
          transform: 'translateX(340px)',
          offset:0,
          opacity: 0,
        }),
        style({
          offset:.7,
          opacity: 1,
          transform: 'translateX(-20px)'
        }),
        style({
          offset: 1,
          transform: 'translateX(0)',
        })
      ]))),
      transition('active => removed', animate('500ms ease-in', keyframes([
        style({
          transform: 'translateX(-20px)',
          opacity: 1,
          offset: .2
        }),
        style({
          opacity:0,
          transform: 'translateX(340px)',
          offset: 1
        })
      ])))
    ]),
  ],
  preserveWhitespaces: false,
})
export class ToastrComponent extends Toast {
  // used for demo purposes
  undoString = 'undo';

  faSquareCheck = faSquareCheck;
  faCheck = faCheck;

  // constructor is only necessary when not using AoT
  // constructor(
  //   protected toastrService: ToastrService,
  //   public toastPackage: ToastPackage,
  // ) {
  //   super(toastrService, toastPackage);
  // }

  action(event: Event) {
    event.stopPropagation();
    this.undoString = 'undid';
    this.toastPackage.triggerAction();
    return false;
  }
}
