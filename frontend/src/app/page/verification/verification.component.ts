import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faEnvelope } from '@fortawesome/free-regular-svg-icons';
import { faSquareEnvelope } from '@fortawesome/free-solid-svg-icons';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.scss']
})
export class VerificationComponent {

  email: string = 'email@default.com';
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
      this.onSubmit();
    }
  }

  onSubmit(): void {
    if (this.otpValue != this.code) {
      this.onDanger('We could not verify your code.')
    } else {
      this.isButtonDisabled = false;
      setTimeout(() => {
        this.router.navigate(['/new-password']);
      }, 500)
    }
  }

  onSuccess(message: string, title: string = 'Success!') {
    this.toastr.showSuccessToast(this.toastRef, title, message);
  }

  onDanger(message: string, title: string = 'Error!') {
    this.toastr.showDangerToast(this.toastRef, title, message);
  }

  onWarning(message: string, title: string = 'Warning!') {
    this.toastr.showWarningToast(this.toastRef, title, message);
  }

  onInfo(message: string, title: string = 'Info!') {
    this.toastr.showInfoToast(this.toastRef, title, message);
  }


}
