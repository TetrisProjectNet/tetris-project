import { Component, HostListener, OnInit } from '@angular/core';
import { faCoffee, faRightToBracket } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {

  faCoffee = faCoffee;
  faRightToBracket = faRightToBracket;

  @HostListener('window:scroll', []) onWindowScroll() {
    // do some stuff here when the window is scrolled
    const verticalOffset =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
  }

  constructor() {}

  ngOnInit(): void {}
}
