import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  emailClass: string = '';
  passwordClass: string = '';

  constructor() { }

  ngOnInit(): void {
  }
  selectedElement: any;
  isEmpty: any = true;

  emailFocusToggler(event: Event): void {
    event.type == 'focus' ? this.emailClass= 'focused' : this.emailClass='';

    if(event) {
      this.selectedElement = event.target;
      console.log(this.selectedElement);
    } else {
      this.selectedElement = null;
    }

    if (this.selectedElement.value != '') {
      this.emailClass= 'focused';
    }
  }

  passwordFocusToggler(event: Event): void {
    event.type == 'focus' ? this.passwordClass= 'focused' : this.passwordClass='';

    if(event) {
      this.selectedElement = event.target;
      console.log(this.selectedElement);
    } else {
      this.selectedElement = null;
    }

    if (this.selectedElement.value != '') {
      this.passwordClass= 'focused';
    }
  }

}
