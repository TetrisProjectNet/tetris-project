import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FaConfig } from '@fortawesome/angular-fontawesome';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import { faCircleChevronDown, faCircleChevronUp, faCircleUser } from '@fortawesome/free-solid-svg-icons';
import { Observable, switchMap, of } from 'rxjs';
import { User } from 'src/app/model/user';
import { AuthService } from 'src/app/service/auth.service';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent {

  user$: Observable<User> = this.activatedRoute.params.pipe(
    switchMap(params => {
      if (params['id']) {
        return this.userService.get(params['id'])
      }
      return of(new User())
    })
  );

  loggedUser$ = this.authService.loggedUser$;
  isAdmin: boolean = false;

  faCircleChevronDown = faCircleChevronDown;
  faCircleChevronUp = faCircleChevronUp;
  faSquareCaretDown = faSquareCaretDown;
  faCircleUser = faCircleUser;

  toastRef: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private toastr: CustomToastrService,
    private authService: AuthService,
    faConfig: FaConfig
  ) {
    faConfig.fixedWidth = false;
  }

  ngOnInit(): void {
    if (this.loggedUser$) {
      this.loggedUser$.subscribe({
        next: (data: any) => {
          if (data?.role == 'admin') {
            this.isAdmin = true;
          }
        }
      })
    }
  }

  onUpdate(user: User): void {
    if (user.id === '') {
      user.joinDate = new Date().toLocaleDateString("en-US");
      this.authService.register(user).subscribe({
        error: err => {
          this.onDanger(err.error, 'Registration failed.');
        },
        complete: () => {
          this.router.navigate(['user']);
          this.onSuccess('User registered.');
        },
      });
    } else {
      this.userService.update(user).subscribe({
        error: err => {
          this.onDanger(err.error, 'Modification failed.');
        },
        complete: () => {
          this.router.navigate(['user']).then(() => location.reload());
          this.onSuccess('User modified.');
        }
      });
    }
  }

  onBanOne(user: User): void {
    if (window.confirm('Are you sure about banning this user?')) {
      this.userService
        .ban(user)
        .subscribe(() => (this.user$ = this.userService.get(user.id)));
    }
  }

  onUnbanOne(user: User): void {
    if (window.confirm('Are you sure about unbanning this user?')) {
      this.userService
        .unban(user)
        .subscribe(() => (this.user$ = this.userService.get(user.id)));
    }
  }

  checkValue(value: any, user: User) {
    if (value === 'true') {
      this.onBanOne(user)
    } else {
      this.onUnbanOne(user);
    }
  }

  getValidationData($event: any) {
    console.log('$event', $event);
  }

  onSuccess(message: string, title: string = 'Success!') {
    this.toastRef = this.toastr.showSuccessToast(title, message);
  }

  onWarning(message: string, title: string = 'Warning!') {
    this.toastRef = this.toastr.showWarningToast(title, message);
  }

  onDanger(message: string, title: string = 'Error!') {
    this.toastRef = this.toastr.showDangerToast(title, message);
  }

  onInfo(message: string, title: string = 'Info!') {
    this.toastRef = this.toastr.showInfoToast(title, message);
  }

  onRemoveToast() {
    this.toastr.removeToast(this.toastRef);
  }

}
