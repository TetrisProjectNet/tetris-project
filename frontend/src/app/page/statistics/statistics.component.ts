import { Component, OnInit } from '@angular/core';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/app/service/user.service';

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
  }

}
