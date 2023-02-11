import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import * as bootstrap from 'bootstrap';
import { Tooltip } from 'bootstrap';
import { FooterComponent } from './common/footer/footer.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'frontend';

  @ViewChildren(FooterComponent) footerComp!: QueryList<FooterComponent>;

  ngOnInit() {
    // var tooltipTriggerList = [].slice.call(
    //   document.querySelectorAll('[data-bs-toggle="tooltip"]')
    // );
    // var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    //   return new bootstrap.Tooltip(tooltipTriggerEl);
    // });
  }

  ngAfterViewInit() {
    this.footerComp.forEach(elem => {
      elem.tooltips.forEach((tooltipNode: any) => {
        new Tooltip(tooltipNode.nativeElement);
      })
    })
  }
}
