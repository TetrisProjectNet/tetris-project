import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faUserCheck, faUserMinus, faUserPen, faUserXmark } from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  // {
  //   "user": [
  //     {
  //       "id": 1,
  //       "username": "bconnew0",
  //       "email": "mbedford0@diigo.com",
  //       "role": "Subcontractor",
  //       "banned": true
  //     },
  //     {
  //       "id": 2,
  //       "username": "ffabler1",
  //       "email": "gparley1@salon.com",
  //       "role": "Construction Foreman",
  //       "banned": true
  //     },
  //     {
  //       "id": 3,
  //       "username": "ecrewther2",
  //       "email": "mpilmore2@paginegialle.it",
  //       "role": "Construction Manager",
  //       "banned": false
  //     },
  //     {
  //       "id": 4,
  //       "username": "csessions3",
  //       "email": "orobottham3@theglobeandmail.com",
  //       "role": "Estimator",
  //       "banned": false
  //     },
  //     {
  //       "id": 5,
  //       "username": "ssmiths4",
  //       "email": "kbatchan4@sohu.com",
  //       "role": "Architect",
  //       "banned": false
  //     },
  //     {
  //       "id": 6,
  //       "username": "jwakely5",
  //       "email": "egavrielli5@i2i.jp",
  //       "role": "Construction Expeditor",
  //       "banned": false
  //     },
  //     {
  //       "id": 7,
  //       "username": "khorick6",
  //       "email": "ryair6@uiuc.edu",
  //       "role": "Engineer",
  //       "banned": false
  //     },
  //     {
  //       "id": 8,
  //       "username": "vleadbeater7",
  //       "email": "mspaduzza7@spiegel.de",
  //       "role": "Project Manager",
  //       "banned": false
  //     },
  //     {
  //       "id": 9,
  //       "username": "wgoldman8",
  //       "email": "jpaskerful8@guardian.co.uk",
  //       "role": "Architect",
  //       "banned": false
  //     },
  //     {
  //       "id": 10,
  //       "username": "skomorowski9",
  //       "email": "mclausen9@archive.org",
  //       "role": "Project Manager",
  //       "banned": false
  //     }
  //   ]
  // }


  list$ = this.userService.getAll();
  entity = 'user';

  faUserPen = faUserPen;
  faUserMinus = faUserMinus;
  faUserXmark = faUserXmark;
  faUserCheck = faUserCheck;

  constructor(
    private userService: UserService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onEditOne(user: User): void {
    this.router.navigate(['/', 'user', user.id])
  }

  onDeleteOne(id: number): void {
    if (window.confirm('Are you sure about deleting this user?')) {
      this.userService.remove(id)
        .subscribe(() => (this.list$ = this.userService.getAll()));
    }
  }

  onBanOne(id: number): void {
    if (window.confirm('Are you sure about banning this user?')) {
      this.userService.ban(id).subscribe(() => (this.list$ = this.userService.getAll()));
    }
  }

  onUnbanOne(id: number): void {
    if (window.confirm('Are you sure about unbanning this user?')) {
      this.userService.unban(id).subscribe(() => (this.list$ = this.userService.getAll()));
    }
  }

}
