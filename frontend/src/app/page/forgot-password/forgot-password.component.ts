import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {

  emailClass: string = '';
  selectedElement: any;

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  focusToggler(event: Event, className: string): string {
    event.type == 'focus' ? className= 'focused' : className='';

    if(event) {
      this.selectedElement = event.target;
    } else {
      this.selectedElement = null;
    }

    if (this.selectedElement.value != '') {
      className= 'focused';
    }
    return className;
  }

  emailFocusToggler(event: Event): void {
    this.emailClass = this.focusToggler(event, this.emailClass)
  }

  onSubmit(email: string): void {
    this.router.navigate(['/verification'], {state: {data: email}})
  }

}
