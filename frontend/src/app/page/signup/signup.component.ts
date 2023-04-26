import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, switchMap, of } from 'rxjs';
import { User } from 'src/app/model/user';
import { AuthService } from 'src/app/service/auth.service';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  user$: Observable<User> = this.activatedRoute.params.pipe(
    switchMap(params => {
      if (params['id']) {
        return this.userService.get(params['id'])
      }
      return of(new User())
    })
  );

  toastRef: any;

  // username: string = '';
  // email: string = '';
  // password: string = ''
  passwordAgain: string = '';

  // usernameClass: string = '';
  // emailClass: string = '';
  // passwordClass: string = '';
  // passwordAgainClass: string = '';
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

  // usernameFocusToggler(event: Event): void {
  //   this.usernameClass = this.focusToggler(event, this.usernameClass)
  // }

  // emailFocusToggler(event: Event): void {
  //   this.emailClass = this.focusToggler(event, this.emailClass)
  // }

  // passwordFocusToggler(event: Event): void {
  //   this.passwordClass = this.focusToggler(event, this.passwordClass)
  // }

  // passwordAgainFocusToggler(event: Event): void {
  //   this.passwordAgainClass = this.focusToggler(event, this.passwordAgainClass)
  // }


  // {
  //   "id": "string",
  //   "username": "string",
  //   "password": "string",
  //   "email": "string",
  //   "role": "string",
  //   "banned": true,
  //   "joinDate": "string",
  //   "lastOnlineDate": "string",
  //   "coins": 0,
  //   "shopItems": [
  //   ],
  //   "scores": [
  //     0
  //   ],
  //   "friends": [
  //     "string"
  //   ]
  // }

  signup(user: User) {
    user.joinDate = new Date().toLocaleDateString("en-US");
    user.coins = 100;
    this.authService.register(user).subscribe({
      error: (error) => {
        if (error.error = 'This username is already in use.') {
          this.onDanger(error, 'Error!');
        } else if (error.error = 'This email address is already registered.') {
          this.onDanger(error, 'Error!');
        } else {
          this.onDanger('Please try again later!', 'Something went wrong.');
        }
      },
      complete: () => {
        this.router.navigate(['/login']);
        this.onSuccess('Your account has been singed up.');
        setTimeout(() => {
          this.onWarning('Don\'t tell your password to anyone!', 'Remember!');
        }, 1000)
      }
    });
  }
  
  onCreate(user: User): void {
    if (user.password !== this.passwordAgain) {
      console.log('Two passwords are not the same!');
    } else {
      console.log(new Date());
      user.joinDate = new Date();
      user.coins = 100;
      user.role = 'player';
      this.userService.create(user)
      .subscribe({
        error: () => this.onDanger('Please try again later!', 'Something went wrong.'),
        complete: () => {
          this.router.navigate(['login']);
          this.onSuccess('Your account has been singed up.');
          setTimeout(() => {
            this.onWarning('Don\'t tell your password to anyone!', 'Remember!');
          }, 1000)
        }
      });

    }
  }

  getValidationData($event: any) {
    console.log('$event', $event);
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
