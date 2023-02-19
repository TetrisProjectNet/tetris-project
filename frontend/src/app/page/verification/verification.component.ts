import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faEnvelope } from '@fortawesome/free-regular-svg-icons';
import { faSquareEnvelope } from '@fortawesome/free-solid-svg-icons';

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
  ) {
    if (history.state.data != '' && history.state.data != undefined) {
      this.email = history.state.data;
    }
  }

  ngOnInit(): void { }

  onOtpChange(event: any) {
    this.otpValue = String(event);
    if (event == this.code) {
      this.onSubmit();
    }
  }

  onSubmit(): void {
    if (this.otpValue != this.code) {

    } else {
      this.isButtonDisabled = false;
      setTimeout(() => {
        this.router.navigate(['/new-password']);
      }, 500)
    }

  }

}
