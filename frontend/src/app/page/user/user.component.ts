import {
  Component,
  ElementRef,
  OnInit,
  QueryList,
  ViewChildren,
} from '@angular/core';
import { Router } from '@angular/router';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import {
  faArrowDownAZ,
  faArrowDownZA,
  faBars,
  faFilter,
  faGavel,
  faKey,
  faTableColumns,
  faUserCheck,
  faUserMinus,
  faUserPen,
  faUserPlus,
  faUserXmark,
} from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { delay } from 'rxjs';
import { User } from 'src/app/model/user';
import { ConfigService } from 'src/app/service/config.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
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

  @ViewChildren('inputRef') inputs!: QueryList<ElementRef>;

  list$ = this.userService.getAll();
  columns = this.config.userTableColumns;
  entity = 'user';

  phrase: string = '';
  filterKey: string = '';

  columnHead: string = '';
  direction: boolean = false;
  sortColumn: string = '';

  draggedColumnIndex: number = 0;
  draggingClass: string = '';

  p: number = 1;
  itemsPerPage: number = 10;

  rowsClass: string = '';
  phraseClass: string = '';
  keyClass: string = '';
  selectedElement: any;

  faUserPen = faUserPen;
  faUserMinus = faUserMinus;
  faUserXmark = faUserXmark;
  faUserCheck = faUserCheck;
  faUserPlus = faUserPlus;
  faGavel = faGavel;
  faBars = faBars;
  faFilter = faFilter;
  faSquareCaretDown = faSquareCaretDown;
  faKey = faKey;
  faTableColumns = faTableColumns;
  faArrowDownAZ = faArrowDownAZ;
  faArrowDownZA = faArrowDownZA;

  toastRef: any;

  constructor(
    private config: ConfigService,
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit() {
    this.inputs.changes.subscribe((sub) => {
      sub.toArray().forEach((element: any) => {
        console.log(element.nativeElement);
        setTimeout(() => {
          if (element.nativeElement.value != '') {
            switch (element.nativeElement.name) {
              // case 'username': {
              //   this.usernameClass = 'focused';
              //   break;
              // }
              // case 'email': {
              //   this.emailClass = 'focused';
              //   break;
              // }
              // case 'role': {
              //   this.roleClass = 'focused';
              //   break;
              // }
              case 'rows': {
                this.rowsClass = 'focused';
                break;
              }
              case 'phrase': {
                this.phraseClass = 'focused';
                break;
              }
              case 'key': {
                this.keyClass = 'focused';
                break;
              }
            }
          }
        }, 1);
      });
    });
  }

  onEditOne(user: User): void {
    this.router.navigate(['/', 'user', user.id]);
  }

  onDeleteOne(id: number): void {
    if (window.confirm('Are you sure about deleting this user?')) {
      // console.log(this.userService.remove(id));
      this.userService.remove(id).subscribe({
        error: () =>
          this.toastr.error('We could not delete this item.', 'Error!'),
        complete: () => {
          (this.list$ = this.userService.getAll()),
            this.toastr.success('Hello world!', 'Toastr fun!');
        },
      });
      console.log('asdasd');
    }
  }

  onBanOne(id: number): void {
    if (window.confirm('Are you sure about banning this user?')) {
      this.userService
        .ban(id)
        .subscribe(() => (this.list$ = this.userService.getAll()));
    }
  }

  onUnbanOne(id: number): void {
    if (window.confirm('Are you sure about unbanning this user?')) {
      this.userService
        .unban(id)
        .subscribe(() => (this.list$ = this.userService.getAll()));
    }
  }

  checkValue(value: any, id: number) {
    if (value === 'true') {
      this.onBanOne(id);
    } else {
      this.onUnbanOne(id);
    }
  }

  focusToggler(event: Event, className: string): string {
    event.type == 'focus' ? (className = 'focused') : (className = '');

    if (event) {
      this.selectedElement = event.target;
    } else {
      this.selectedElement = null;
    }

    if (this.selectedElement.value != '') {
      className = 'focused';
    }
    return className;
  }

  rowsFocusToggler(event: Event): void {
    this.rowsClass = this.focusToggler(event, this.rowsClass);
  }

  phraseFocusToggler(event: Event): void {
    this.phraseClass = this.focusToggler(event, this.phraseClass);
  }

  keyFocusToggler(event: Event): void {
    this.keyClass = this.focusToggler(event, this.keyClass);
  }

  onColumnSelect(columnHead: string): void {
    this.sortColumn = columnHead;
    this.direction = !this.direction;
  }

  public arrayMove(arr: any[], from: number, to: any) {
    let cutOut = arr.splice(from, 1)[0]; // remove the dragged element at index 'from'
    arr.splice(to, 0, cutOut); // insert it at index 'to'
  }

  public dragStartColumn(index: any) {
    this.draggingClass = 'dragged__item';
    this.draggedColumnIndex = index;
  }

  public allowDrop(event: any) {
    this.draggingClass = '';
    event.preventDefault();
  }

  public dropColumn(index: any) {
    this.arrayMove(this.columns, this.draggedColumnIndex, index);
  }

  showSuccess() {
    this.toastr.show('Hello world!', 'Toastr fun!');
  }

  //   openNotyf() {
  //   const opt = cloneDeep(this.options);
  //   opt.toastComponent = NotyfToast;
  //   opt.toastClass = 'notyf confirm';
  //   // opt.positionClass = 'notyf__wrapper';
  //   // this.options.newestOnTop = false;
  //   const { message, title } = this.getMessage();
  //   const inserted = this.toastr.show(message || 'Success', title, opt);
  //   if (inserted && inserted.toastId) {
  //     this.lastInserted.push(inserted.toastId);
  //   }
  //   return inserted;
  // }
  showToast=()=>{
    console.log('asd');
    this.toastRef = this.toastr.show("New user registered.", 'Success!',{
      disableTimeOut: false,
      tapToDismiss: false,
      toastClass: "custom-toast-success",
      closeButton: true,
      progressBar: true,
      positionClass:'toast-bottom-left',
      timeOut: 50000,
      extendedTimeOut: 50000
    });
    console.log(this.toastRef);
  }

  removeToast = () =>{
    this.toastr.clear(this.toastRef.ToastId);
  }

}
