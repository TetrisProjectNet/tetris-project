import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import * as bootstrap from 'bootstrap';
import { Tooltip } from 'bootstrap';
import { FooterComponent } from './common/footer/footer.component';
import { User } from './model/user';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  @ViewChildren(FooterComponent) footerComp!: QueryList<FooterComponent>;

  title = 'frontend';
  user = new User();

  constructor(
    private authService: AuthService
  ) { }
  
  ngOnInit() {
    // var tooltipTriggerList = [].slice.call(
    //   document.querySelectorAll('[data-bs-toggle="tooltip"]')
    // );
    // var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    //   return new bootstrap.Tooltip(tooltipTriggerEl);
    // });
  }

  ngAfterViewInit() {
    // this.footerComp.forEach(elem => {
    //   elem.tooltips.forEach((tooltipNode: any) => {
    //     new Tooltip(tooltipNode.nativeElement);
    //   })
    // })
  }

  



}
