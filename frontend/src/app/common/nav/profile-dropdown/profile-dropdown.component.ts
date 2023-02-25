import { Component, Input } from '@angular/core';
import { faCircleUser, faRightFromBracket, faUser, faUserGear, faUserShield } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-profile-dropdown',
  templateUrl: './profile-dropdown.component.html',
  styleUrls: ['./profile-dropdown.component.scss']
})
export class ProfileDropdownComponent {

  @Input() userIconClass: string = '';

  faCircleUser = faCircleUser;
  faUser = faUser;
  faUserGear = faUserGear;
  faUserShield = faUserShield;
  faRightFromBracket = faRightFromBracket;

}
