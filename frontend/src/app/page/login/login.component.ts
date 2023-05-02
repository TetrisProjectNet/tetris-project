import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loggedUser$ = this.authService.loggedUser$;

  loginData = {
    username: 'asd',
    password: 'asdD1'
  };

  username: string = '';
  password: string = '';

  toastRef: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private toastr: CustomToastrService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {  }

  login(username: string, password: string) {
    this.authService.login(username, password).subscribe({
      next: (token: string) => {
        localStorage.setItem('authToken', token);
      },
      error: (error) => {
        if (error.error == 'Error.') {
          this.onDanger('Wrong username or password.', 'Invalid data.');
        } else if (error.error == 'This account is banned.') {
          this.onWarning('Bans are permanent.', 'Account is banned!');
        } else {
          this.onDanger('Please try again later!', 'Something went wrong.');
        }
      },
      complete: () => {
        this.router.navigate(['/home']),
        this.onSuccess('Successfully logged in!');
        this.authService.setLoginData();
        setTimeout(() => {
          if (this.authService.isLogged) {
            this.onWarning('Don\'t tell your password to anyone!', 'Remember!');
          }
        }, 6000)
      }
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
