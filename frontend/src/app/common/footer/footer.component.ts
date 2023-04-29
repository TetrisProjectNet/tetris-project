import { Component, ElementRef, OnInit, QueryList, ViewChildren } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { faAngular, faBootstrap, faFontAwesome, faGithub, faGoogle, faInstagram, faLinkedinIn, faSass, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { faChartPie, faCode, faDatabase, faGamepad, faLocationDot, faPhone } from '@fortawesome/free-solid-svg-icons';
import { faEnvelopeOpen, faFileCode } from '@fortawesome/free-regular-svg-icons';
import { FaConfig } from '@fortawesome/angular-fontawesome';
import { NgxPopperjsTriggers, NgxPopperjsPlacements } from 'ngx-popperjs';

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

  @ViewChildren('tooltipRef') tooltipElements!: QueryList<ElementRef>;
  tooltips: any;

  triggers = NgxPopperjsTriggers;
  placements = NgxPopperjsPlacements;
  popperStyles: any = {
    'background-color': '#070707',
    'padding': '6px 10px',
    'border-radius': '4px'
  }

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
  faDatabase = faDatabase;
  faCode = faCode;
  faGamepad = faGamepad;
  faFileCode = faFileCode;
  faFontAwesome = faFontAwesome;
  faBootstrap = faBootstrap;
  faChartPie = faChartPie;

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

  ngAfterViewInit() {
    this.tooltips = this.tooltipElements;
  }

  goToBottom() {
    window.scrollTo(10,document.body.scrollHeight);
  }

}
