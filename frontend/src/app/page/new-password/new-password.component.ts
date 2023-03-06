import { Component } from '@angular/core';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.scss']
})
export class NewPasswordComponent {

  password: string = ''
  passwordAgain: string = '';

  toastRef: any;

  // passwordClass: string = '';
  // passwordAgainClass: string = '';
  // selectedElement: any;

  constructor(
    private toastr: CustomToastrService,
  ) { }

  ngOnInit(): void {
    setTimeout(() => {
      this.onWarning('Don\'t tell your password to anyone!');
    }, 1000)
  }

  // focusToggler(event: Event, className: string): string {
  //   event.type == 'focus' ? className= 'focused' : className='';

  //   if(event) {
  //     this.selectedElement = event.target;
  //   } else {
  //     this.selectedElement = null;
  //   }

  //   if (this.selectedElement.value != '') {
  //     className= 'focused';
  //   }
  //   return className;
  // }

  // passwordFocusToggler(event: Event): void {
  //   this.passwordClass = this.focusToggler(event, this.passwordClass)
  // }

  // passwordAgainFocusToggler(event: Event): void {
  //   this.passwordAgainClass = this.focusToggler(event, this.passwordAgainClass)
  // }

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
