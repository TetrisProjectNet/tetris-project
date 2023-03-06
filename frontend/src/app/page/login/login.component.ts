import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginData = {
    username: 'asd',
    password: 'asdD1'
  };

  username: string = '';
  password: string = '';

  toastRef: any;

  // emailClass: string = '';
  // passwordClass: string = '';
  // selectedElement: any;

  constructor(
    private router: Router,
    private toastr: CustomToastrService,
  ) { }

  ngOnInit(): void {
  }

  onSubmit(username: string, password: string): void {
    if (this.username == this.loginData.username && this.password == this.loginData.password){
      this.onRemoveToast();
      // this.router.navigate(['/home']);
    } else {
      this.onDanger('Incorrect username or password.')

    }

    // this.onSuccess('It will be live for 30 mins.', 'Code sent.');
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

  // emailFocusToggler(event: Event): void {
  //   this.emailClass = this.focusToggler(event, this.emailClass)
  // }

  // passwordFocusToggler(event: Event): void {
  //   this.passwordClass = this.focusToggler(event, this.passwordClass)
  // }

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

  onRemoveToast() {
    console.log(this.toastRef);
    // this.toastr.removeToast(this.toastRef);
  }

}
