import { Component, ElementRef, Input, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FaConfig } from '@fortawesome/angular-fontawesome';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import { faCircleChevronDown, faCircleChevronUp, faCircleUser } from '@fortawesome/free-solid-svg-icons';
import { Observable, switchMap, of } from 'rxjs';
import { FloatingLabelInputComponent } from 'src/app/common/floating-label-input/floating-label-input.component';
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

  isButtonDisabled: boolean = true;

  faCircleChevronDown = faCircleChevronDown;
  faCircleChevronUp = faCircleChevronUp;
  faSquareCaretDown = faSquareCaretDown;
  faCircleUser = faCircleUser;

  clicked: boolean = false;

  toastRef: any;

  // usernameClass: string = '';
  // emailClass: string = '';
  // roleClass: string = '';
  // coinsClass: string = '';
  // selectedElement: any;

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

  ngOnInit(): void { }

  ngAfterViewInit() {
  //   this.inputs.changes.subscribe(sub => {
  //     sub.toArray().forEach((element: any) => {
  //       console.log(element.nativeElement);
  //       setTimeout(() => {

  //         if (element.nativeElement.value != '') {
  //           switch (element.nativeElement.name) {
  //             case 'username': {
  //               this.usernameClass = 'focused';
  //               break;
  //             }
  //             case 'email': {
  //               this.emailClass = 'focused';
  //               break;
  //             }
  //             case 'role': {
  //               this.roleClass = 'focused';
  //               break;
  //             }
  //             case 'coins': {
  //               this.coinsClass = 'focused';
  //               break;
  //             }
  //           }
  //         }

  //       }, 1)
  //     })
  //   });
  // }

  // focusToggler(event: Event, className: string): string {
  //   event.type == 'focus' ? className = 'focused' : className = '';

  //   if (event) {
  //     this.selectedElement = event.target;
  //   } else {
  //     this.selectedElement = null;
  //   }

  //   if (this.selectedElement.value != '') {
  //     className = 'focused';
  //   }
  //   return className;
  // }

  // usernameFocusToggler(event: Event): void {
  //   this.usernameClass = this.focusToggler(event, this.usernameClass)
  // }

  // emailFocusToggler(event: Event): void {
  //   this.emailClass = this.focusToggler(event, this.emailClass)
  // }

  // roleFocusToggler(event: Event): void {
  //   this.roleClass = this.focusToggler(event, this.roleClass)
  // }

  // coinsFocusToggler(event: Event): void {
  //   this.coinsClass = this.focusToggler(event, this.coinsClass)
  }

  onUpdate(form: NgForm, user: User): void {
    console.log(user);
    this.clicked = true;
    if (user.id === '') {
      user.joinDate = new Date().toLocaleDateString("en-US");
      this.authService.register(user).subscribe({
        error: err => {
          this.onDanger('Registration failed.<br>Please try again later!');
          console.error(err);
        },
        complete: () => {
          this.router.navigate(['user']);
          this.onSuccess('User registered.');
        },
      });
      // this.userService.create(user).subscribe(
      //   err => console.error(err),
      //   () => this.router.navigate(['user']),
      // );
    } else {
      this.userService.update(user).subscribe({
        error: err => {
          console.error(err);
          this.onDanger('Modification failed.<br>Please try again later!');
        },
        complete: () => {
          this.router.navigate(['user']);
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
