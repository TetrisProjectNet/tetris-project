import { Component, HostListener, OnInit } from '@angular/core';
import { FaConfig } from '@fortawesome/angular-fontawesome';
import { faBars, faCircleUser, faCoins, faRightFromBracket, faRightToBracket, faUser, faUserGear, faUserShield } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})

export class NavbarComponent implements OnInit {

  loggedUser$ = this.authService.loggedUser$;
  alwaysTrue: boolean = true;
  innerWidth: any;
  breakpoint: number = 992;

  faRightToBracket = faRightToBracket;
  faRightFromBracket = faRightFromBracket;
  faBars = faBars;
  faCoins = faCoins;

  coins = 100;

  className: string = '';

  @HostListener('window:resize', []) onResize() {
    this.innerWidth = window.innerWidth;
  }

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
    faConfig: FaConfig,
    private authService: AuthService,
  ) {
    faConfig.fixedWidth = true;
  }

  ngOnInit(): void {
    this.innerWidth = window.innerWidth;
  }
}
