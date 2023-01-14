import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { faAngular, faGithub, faGoogle, faInstagram, faLinkedinIn, faSass, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { faLocationDot, faPhone } from '@fortawesome/free-solid-svg-icons';
import { faEnvelopeOpen } from '@fortawesome/free-regular-svg-icons';
import { FaConfig } from '@fortawesome/angular-fontawesome';

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
  faTwitter = faTwitter;
  faGoogle = faGoogle;
  faInstagram = faInstagram;
  faAngular = faAngular;
  faSass = faSass;
  faLocationDot = faLocationDot;
  faEnvelopeOpen = faEnvelopeOpen;
  faPhone = faPhone;

  isOpen = true;

  toggle() {
    this.isOpen = !this.isOpen;
  }

  constructor(
    faConfig: FaConfig
  ) {
    faConfig.fixedWidth = true;
  }

  ngOnInit(): void {
  }

  goToBottom() {
    window.scrollTo(10,document.body.scrollHeight);
  }

}
