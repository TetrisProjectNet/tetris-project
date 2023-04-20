import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faEnvelope } from '@fortawesome/free-regular-svg-icons';
import { faSquareEnvelope } from '@fortawesome/free-solid-svg-icons';
import { throwError } from 'rxjs';
import { Verification } from 'src/app/model/verification';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { VerificationService } from 'src/app/service/verification.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.scss']
})
export class VerificationComponent {

  // email: string = 'email@default.com';
  email: string = 'nagyklevi@gmail.com';
  code: string = '123456';
  otpValue: string = '';
  isButtonDisabled: boolean = true;
  toastRef: any;

  config = {
    length: 6,
    allowNumbersOnly: true,
    isPasswordInput: false,
    disableAutoFocus: true,
    placeholder: '',
    inputClass: 'ng-otp-input',
    containerClass: 'ng-otp-input-wrapper',
  };

  faSquareEnvelope = faSquareEnvelope;
  faEnvelope = faEnvelope;

  constructor(
    private router: Router,
    private toastr: CustomToastrService,
    private verificationService: VerificationService,
  ) {
    if (history.state.data != '' && history.state.data != undefined) {
      this.email = history.state.data;
    }
  }

  ngOnInit(): void { }

  onOtpChange(event: any) {
    this.otpValue = String(event);
    console.log(event.length);
    if (event.length == this.config.length) {
      this.isButtonDisabled = false;
      this.onSubmit();
    }
  }

  onSubmit(): void {
    // let emailsObj = new Verification();
    this.verificationService.getBasedOnEmail(this.email)
    .subscribe({
      next: data => {
        console.log(data)
        if (data) {
          if (this.otpValue == data.code) {
            this.router.navigate(['/new-password'], {state: {data: this.email, code: this.otpValue}});
          } else {
            this.onDanger('We could not verify your code.');
          }
        } else {
          this.onDanger('We could not check your code.<br>Please try again later!', 'Something went wrong.');
        }
      },
      error: () => this.onDanger('We could not check your code.<br>Please try again later!', 'Something went wrong.'),
      // complete: () => {
      //   this.router.navigate(['shop']);
      //   this.onSuccess('Shop item updated.');
      // }
    });

    // console.log(emailsObj);

    // if (this.otpValue != this.code) {
    //   this.onDanger('We could not verify your code.')
    // } else {
    //   this.isButtonDisabled = false;
    //   setTimeout(() => {
    //     this.router.navigate(['/new-password']);
    //   }, 500)
    // }
  }

  // toast functions
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
