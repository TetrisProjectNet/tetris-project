import { Component, QueryList, ViewChildren } from '@angular/core';
import { FooterComponent } from './common/footer/footer.component';
import { User } from './model/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  @ViewChildren(FooterComponent) footerComp!: QueryList<FooterComponent>;

  title = 'Tetris-project';
  user = new User();

  constructor() { }
  
  ngOnInit() {

  }

}
