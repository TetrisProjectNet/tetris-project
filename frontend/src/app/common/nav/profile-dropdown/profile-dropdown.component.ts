import { Component, Input } from '@angular/core';
import { faCircleUser, faRightFromBracket, faUser, faUserGear, faUserShield } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/service/auth.service';

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
  ) {  }


  logout() {
    localStorage.removeItem('authToken');
  }

}
