import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { faGithub, faLinkedinIn } from '@fortawesome/free-brands-svg-icons';

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

  faGithub = faGithub;
  faLinkedinIn = faLinkedinIn;

  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }

  constructor() { }

  ngOnInit(): void {
  }

  goToBottom() {
    window.scrollTo(10,document.body.scrollHeight);
  }

}
