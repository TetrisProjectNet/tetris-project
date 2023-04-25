import { Component, Input } from '@angular/core';
import { faCircleUser, faRightFromBracket, faUser, faUserGear, faUserShield } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/service/auth.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-profile-dropdown',
  templateUrl: './profile-dropdown.component.html',
  styleUrls: ['./profile-dropdown.component.scss']
})
export class ProfileDropdownComponent {

  loggedUser$ = this.authService.loggedUser$;

  @Input() userIconClass: string = '';

  faCircleUser = faCircleUser;
  faUser = faUser;
  faUserGear = faUserGear;
  faUserShield = faUserShield;
  faRightFromBracket = faRightFromBracket;

  constructor(
    private authService: AuthService,
    private userService: UserService,
  ) {  }


  logout() {
    this.loggedUser$.subscribe({
      next: (user: any) => {
          // console.log(user);
          if (user) {
            // user.refreshToken = '';
            user.lastOnlineDate = new Date();
            this.userService.update(user).subscribe({
              complete: () => {
                localStorage.removeItem('authToken');
                this.authService.resetLoginData();
              }
            })
          }
      }
    })
  }

}
