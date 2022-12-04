import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  userNameClass: string = '';
  emailClass: string = '';
  passwordClass: string = '';
  passwordAgainClass: string = '';
  selectedElement: any;

  constructor() { }

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

  userNameFocusToggler(event: Event): void {
    this.userNameClass = this.focusToggler(event, this.userNameClass)
  }

  emailFocusToggler(event: Event): void {
    this.emailClass = this.focusToggler(event, this.emailClass)
  }

  passwordFocusToggler(event: Event): void {
    this.passwordClass = this.focusToggler(event, this.passwordClass)
  }

  passwordAgainFocusToggler(event: Event): void {
    this.passwordAgainClass = this.focusToggler(event, this.passwordAgainClass)
  }
}
