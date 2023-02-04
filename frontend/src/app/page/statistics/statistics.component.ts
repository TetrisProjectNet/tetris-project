import { Component, OnInit } from '@angular/core';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/app/service/user.service';
import * as AOS from 'aos';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss']
})
export class StatisticsComponent implements OnInit {

  list$ = this.userService.getAll();


  faStar = faStar;

  constructor(
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    AOS.init({
      duration: 600,
      offset: 130,
      easing: 'ease-in-sine'
    });
  }

}
