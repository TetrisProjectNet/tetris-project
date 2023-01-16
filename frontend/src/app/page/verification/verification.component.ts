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

  email: string = history.state.data;
  config = {
    length: 6,
    allowNumbersOnly: true,
    isPasswordInput: false,
    disableAutoFocus: true,
    placeholder: '',
    inputClass: 'ng-otp-input',
    containerClass: 'ng-otp-input-wrapper',
    inputStyles: {
      // 'width': '50px',
      // 'height': '50px'
    }
  };

  faSquareEnvelope = faSquareEnvelope;
  faEnvelope = faEnvelope;

  constructor(
    private router: Router,
  ) { }

  ngOnInit(): void { }

  onSubmit(): void {
    this.router.navigate(['/new-password'])
  }

}
