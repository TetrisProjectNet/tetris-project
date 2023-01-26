import { Component, HostListener, OnInit } from '@angular/core';
import { faBars, faCoins, faRightToBracket } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})

export class HeaderComponent implements OnInit {

  faRightToBracket = faRightToBracket;
  faBars = faBars;
  faCoins = faCoins;
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

  constructor() {}

  ngOnInit(): void {}
}
