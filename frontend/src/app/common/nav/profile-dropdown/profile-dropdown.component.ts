import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
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
    private router: Router,
  ) {  }

  logout() {
    this.loggedUser$.subscribe({
      next: (user: any) => {
          if (user) {
            user.lastOnlineDate = new Date().toLocaleDateString("en-US");
            
            this.userService.update(user).subscribe({
              error: err => console.log(err),
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
