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
import { User } from 'src/app/model/user';
import { ConfigService } from 'src/app/service/config.service';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {

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
    // private toastr: ToastrService,
    private toastr: CustomToastrService,
  ) { }

  ngOnInit(): void { }


  onEditOne(user: User): void {
    this.router.navigate(['/', 'user', user.id]);
  }

  onDeleteOne(id: string): void {
    if (window.confirm('Are you sure about deleting this user?')) {
      this.userService.remove(id).subscribe({
        error: () =>
          this.onDanger('We could not delete this user.<br>Please try again later!', 'Something went wrong.'),
        complete: () => {
          this.list$ = this.userService.getAll();
          this.onSuccess('User deleted.');
        },
      });
    }
  }

  onBanOne(user: User): void {
    if (window.confirm('Are you sure about banning this user?')) {
      this.userService
        .ban(user)
        .subscribe({
          error: () =>
          this.onDanger('We could not ban this user.<br>Please try again later!', 'Something went wrong.'),
          complete: () => {
            this.list$ = this.userService.getAll();
            this.onSuccess('User banned.');
          }
        });
    }
  }

  onUnbanOne(user: User): void {
    if (window.confirm('Are you sure about unbanning this user?')) {
      this.userService
        .unban(user)
        .subscribe({
          error: () =>
          this.onDanger('We could not unban this user.<br>Please try again later!', 'Something went wrong.'),
          complete: () => {
            this.list$ = this.userService.getAll();
            this.onSuccess('User unbanned.');
          }
        });
    }
  }

  checkValue(value: any, user: User) {
    if (value === 'true') {
      this.onBanOne(user);
    } else {
      this.onUnbanOne(user);
    }
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

  onSuccess(message: string, title: string = 'Success!') {
    this.toastRef = this.toastr.showSuccessToast(title, message);
  }

  onDanger(message: string, title: string = 'Error!') {
    this.toastRef = this.toastr.showDangerToast(title, message);
  }

  onWarning(message: string, title: string = 'Warning!') {
    this.toastRef = this.toastr.showWarningToast(title, message);
  }

  onInfo(message: string, title: string = 'Info!') {
    this.toastRef = this.toastr.showInfoToast(title, message);
  }

  onRemoveToast() {
    this.toastr.removeToast(this.toastRef);
  }

}
