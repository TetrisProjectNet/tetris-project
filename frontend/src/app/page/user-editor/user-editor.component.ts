import { Component, ElementRef, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FaConfig } from '@fortawesome/angular-fontawesome';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import { faCircleChevronDown, faCircleChevronUp, faCircleUser } from '@fortawesome/free-solid-svg-icons';
import { Observable, switchMap, of } from 'rxjs';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent {

  // @ViewChild('inputRef') input!: ElementRef;
  @ViewChildren('inputRef') inputs!: QueryList<ElementRef>;

  user$: Observable<User> = this.activatedRoute.params.pipe(
    switchMap(params => {
      if (params['id']) {
        return this.userService.get(params['id'])
      }
      return of(new User())
    })
  );

  faCircleChevronDown = faCircleChevronDown;
  faCircleChevronUp = faCircleChevronUp;
  faSquareCaretDown = faSquareCaretDown;
  faCircleUser = faCircleUser;

  clicked: boolean = false;

  usernameClass: string = '';
  emailClass: string = '';
  roleClass: string = '';
  coinsClass: string = '';
  selectedElement: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    faConfig: FaConfig
  ) {
    faConfig.fixedWidth = false;
  }

  ngOnInit(): void { }

  ngAfterViewInit() {
    this.inputs.changes.subscribe(sub => {
      sub.toArray().forEach((element: any) => {
        console.log(element.nativeElement);
        setTimeout(() => {

          if (element.nativeElement.value != '') {
            switch (element.nativeElement.name) {
              case 'username': {
                this.usernameClass = 'focused';
                break;
              }
              case 'email': {
                this.emailClass = 'focused';
                break;
              }
              case 'role': {
                this.roleClass = 'focused';
                break;
              }
              case 'coins': {
                this.coinsClass = 'focused';
                break;
              }
            }
          }

        }, 1)
      })
    });
  }

  focusToggler(event: Event, className: string): string {
    event.type == 'focus' ? className = 'focused' : className = '';

    if (event) {
      this.selectedElement = event.target;
    } else {
      this.selectedElement = null;
    }

    if (this.selectedElement.value != '') {
      className = 'focused';
    }
    return className;
  }

  usernameFocusToggler(event: Event): void {
    this.usernameClass = this.focusToggler(event, this.usernameClass)
  }

  emailFocusToggler(event: Event): void {
    this.emailClass = this.focusToggler(event, this.emailClass)
  }

  roleFocusToggler(event: Event): void {
    this.roleClass = this.focusToggler(event, this.roleClass)
  }

  coinsFocusToggler(event: Event): void {
    this.coinsClass = this.focusToggler(event, this.coinsClass)
  }

  onUpdate(form: NgForm, user: User): void {
    this.clicked = true;
    if (user.id === 0) {
      this.userService.create(form.value).subscribe(
        () => this.router.navigate(['user']),
        err => console.error(err)
      );
    } else {
      this.userService.update(user).subscribe(
        () => this.router.navigate(['user']),
        err => console.error(err)
      );
    }
  }

  onBanOne(id: number): void {
    if (window.confirm('Are you sure about banning this user?')) {
      this.userService
        .ban(id)
        .subscribe(() => (this.user$ = this.userService.get(id)));
    }
  }

  onUnbanOne(id: number): void {
    if (window.confirm('Are you sure about unbanning this user?')) {
      this.userService
        .unban(id)
        .subscribe(() => (this.user$ = this.userService.get(id)));
    }
  }

  checkValue(value: any, id: number) {
    if (value === 'true') {
      this.onBanOne(id)
    } else {
      this.onUnbanOne(id);
    }
  }


}
