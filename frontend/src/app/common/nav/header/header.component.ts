import { Component, HostListener, OnInit } from '@angular/core';
import { FaConfig } from '@fortawesome/angular-fontawesome';
import { faBars, faCircleUser, faCoins, faRightFromBracket, faRightToBracket, faUser, faUserGear, faUserShield } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})

export class HeaderComponent implements OnInit {

  faRightToBracket = faRightToBracket;
  faRightFromBracket = faRightFromBracket;
  faBars = faBars;
  faCoins = faCoins;
  faCircleUser = faCircleUser;
  faUser = faUser;
  faUserGear = faUserGear;
  faUserShield = faUserShield;

  coins = 100;

  className: string = '';


  @HostListener('window:scroll', []) onWindowScroll() {
    // do some stuff here when the window is scrolled
    // console.log('asd');
    const verticalOffset =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
      // console.log(verticalOffset);

      verticalOffset != 0 ? this.className = 'scroll' : this.className = '';
    // if () {
    //   this.className = 'scroll';
    // }
  }

  constructor(
    faConfig: FaConfig
  ) {
    faConfig.fixedWidth = true;
  }

  ngOnInit(): void {}
}
