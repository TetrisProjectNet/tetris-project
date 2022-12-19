import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import { faCircleChevronDown } from '@fortawesome/free-solid-svg-icons';
import { Observable, switchMap, of } from 'rxjs';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent {

  faCircleChevronDown = faCircleChevronDown;
  faSquareCaretDown = faSquareCaretDown;

  user$: Observable<User> = this.activatedRoute.params.pipe(
    switchMap(params => {
      if (params['id']) {
        return this.userService.get(params['id'])
      }
      return of(new User())
    })
  );

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
  ) { }

  ngOnInit(): void {
    // this.usernameFocusToggler('clicked');
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

}
