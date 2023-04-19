import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of, switchMap } from 'rxjs';
import { User } from 'src/app/model/user';
import { AuthService } from 'src/app/service/auth.service';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  // user$: Observable<User> = this.activatedRoute.params.pipe(
  //   switchMap(params => {
  //     if (params['id']) {
  //       return this.userService.get(params['id'])
  //     }
  //     return of(new User())
  //   })
  // );

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
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private toastr: CustomToastrService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
  }

  login(username: string, password: string) {
    // this.authService.login(username, password).subscribe((token: string) => {
    //   console.log(token);
    //   localStorage.setItem('authToken', token);
    // });
    this.authService.login(username, password).subscribe({
      next: (token: string) => {
        localStorage.setItem('authToken', token);
      },
      error: (error) => {this.onDanger('Please try again later!', 'Something went wrong.'), console.log(error);},
      complete: () => {
        // this.router.navigate(['login']);
        this.onSuccess('Successfully logged in!');
        setTimeout(() => {
          this.onWarning('Don\'t tell your password to anyone!', 'Remember!');
        }, 1000)
      }
    });
  }

  getMe() {
    this.authService.getMe().subscribe((response: any) => {
      console.log(response);
    });
  }



  onSubmit(username: string, password: string): void {
    if (this.username == this.loginData.username && this.password == this.loginData.password){
      this.onRemoveToast();
      this.router.navigate(['/home']);
    } else {
      this.onDanger('Incorrect username or password.')
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

  // emailFocusToggler(event: Event): void {
  //   this.emailClass = this.focusToggler(event, this.emailClass)
  // }

  // passwordFocusToggler(event: Event): void {
  //   this.passwordClass = this.focusToggler(event, this.passwordClass)
  // }

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
