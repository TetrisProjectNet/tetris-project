import { Component, OnInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from '@angular/animations';


@Component({
  selector: 'app-footer',
  animations: [
    trigger('openClose', [
      // ...
      state('open', style({
        display: 'block',
        // position: 'absolute',
      })),
      state('closed', style({
        display: 'none',
        // position: 'absolute',
        // bottom: '-100px'
      })),
      transition('open => closed', [
        animate('0s')
      ]),
      transition('closed => open', [
        animate('0s')
      ]),
    ]),
  ],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }

  constructor() { }

  ngOnInit(): void {
  }

}
