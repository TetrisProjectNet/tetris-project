import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.scss']
})
export class NewPasswordComponent {

  email: string = '';
  code: string = '';

  password: string = ''
  passwordAgain: string = '';

  toastRef: any;

  // passwordClass: string = '';
  // passwordAgainClass: string = '';
  // selectedElement: any;

  constructor(
    private toastr: CustomToastrService,
    private router: Router,
    private authService: AuthService,

  ) {
    if (history.state.data != '' && history.state.data != undefined) {
      this.email = history.state.data;
    }
    if (history.state.code != '' && history.state.code != undefined) {
      this.code = history.state.code;
    }
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.onWarning('Don\'t tell your password to anyone!');
    }, 1000)
  }

  onSubmit(password: string, passwordAgain: string): void {
    // let emailsObj = new Verification();
    if (password !== passwordAgain) {
      this.onDanger('Password and password again do not match.');
    } else {

      this.authService.resetPassword(this.email, password, this.code)
      .subscribe({
        // next: data => {
        //   console.log(data)
        //   if (this.otpValue == data.code) {
        //     this.router.navigate(['/new-password'], {state: {data: this.email}});
        //   } else {
        //     this.onDanger('We could not verify your code.');
        //   }
        // },
        error: () => {
          this.onDanger('We could not change your password.<br>Please send us your email again!');
          this.router.navigate(['/forgot-password']);
        },
        complete: () => {
          // this.router.navigate(['shop']);
          this.onSuccess('Your password has been changed.');
        }
      });

    }

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
