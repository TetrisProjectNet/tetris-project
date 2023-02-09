import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import { Tooltip } from 'bootstrap';
import { FooterComponent } from './common/footer/footer.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'frontend';

  @ViewChildren(FooterComponent) footerComp!:QueryList<FooterComponent>;

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.footerComp.forEach(elem => {
      elem.tooltips.forEach((tooltipNode: any) => {
        new Tooltip(tooltipNode.nativeElement);
      })
    })
  }

}
