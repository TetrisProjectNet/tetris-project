import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faUserMinus, faUserPen, faUserXmark } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  users: any = [
    {
      "id": 1,
      "userName": "bconnew0",
      "email": "mbedford0@diigo.com",
      "role": "Subcontractor"
    }, {
      "id": 2,
      "userName": "ffabler1",
      "email": "gparley1@salon.com",
      "role": "Construction Foreman"
    }, {
      "id": 3,
      "userName": "ecrewther2",
      "email": "mpilmore2@paginegialle.it",
      "role": "Construction Manager"
    }, {
      "id": 4,
      "userName": "csessions3",
      "email": "orobottham3@theglobeandmail.com",
      "role": "Estimator"
    }, {
      "id": 5,
      "userName": "ssmiths4",
      "email": "kbatchan4@sohu.com",
      "role": "Architect"
    }, {
      "id": 6,
      "userName": "jwakely5",
      "email": "egavrielli5@i2i.jp",
      "role": "Construction Expeditor"
    }, {
      "id": 7,
      "userName": "khorick6",
      "email": "ryair6@uiuc.edu",
      "role": "Engineer"
    }, {
      "id": 8,
      "userName": "vleadbeater7",
      "email": "mspaduzza7@spiegel.de",
      "role": "Project Manager"
    }, {
      "id": 9,
      "userName": "wgoldman8",
      "email": "jpaskerful8@guardian.co.uk",
      "role": "Architect"
    }, {
      "id": 10,
      "userName": "skomorowski9",
      "email": "mclausen9@archive.org",
      "role": "Project Manager"
    }
  ]

  faUserPen = faUserPen;
  faUserMinus = faUserMinus;
  faUserXmark = faUserXmark;

  constructor(
    private router: Router

  ) {}

  ngOnInit(): void {
  }

  onEditOne(user: any): void {
    this.router.navigate(['/', 'user', user.id])
  }

}
