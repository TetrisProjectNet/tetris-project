import { Component } from '@angular/core';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.scss']
})
export class NewPasswordComponent {

  password: string = ''
  passwordAgain: string = '';

  // passwordClass: string = '';
  // passwordAgainClass: string = '';
  // selectedElement: any;

  constructor() { }

  ngOnInit(): void {
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

}
