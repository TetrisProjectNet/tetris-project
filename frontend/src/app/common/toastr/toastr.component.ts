import { Component, HostListener } from '@angular/core';
import { faCircleCheck, faCircleXmark, } from '@fortawesome/free-regular-svg-icons';
import { faCircleExclamation, faCircleInfo, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Toast } from 'ngx-toastr';
import { getAnimations } from '../animations/toastr.animation';

@Component({
  selector: '[app-toastr]',
  templateUrl: './toastr.component.html',
  styleUrls: ['./toastr.component.scss'],
  animations: getAnimations(ToastrComponent.prototype.onResize()),
  preserveWhitespaces: false,
})

export class ToastrComponent extends Toast {

  faCircleCheck = faCircleCheck;
  faCircleXmark = faCircleXmark;
  faCircleInfo = faCircleInfo;
  faCircleExclamation = faCircleExclamation;
  faXmark = faXmark;

  @HostListener('window:resize', ['$event'])
  onResize() {
    if (matchMedia('(max-width: 600px)').matches) {
      return true;
    } else {
      return false;
    }
  }

  getAnimationType() {
    return this.onResize();
  }

}
