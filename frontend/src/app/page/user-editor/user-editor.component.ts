import { Component, ElementRef, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faSquareCaretDown } from '@fortawesome/free-regular-svg-icons';
import { faCircleChevronDown } from '@fortawesome/free-solid-svg-icons';
import { Observable, switchMap, of } from 'rxjs';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent {

  // @ViewChild('inputRef') input!: ElementRef;
  @ViewChildren('inputRef') inputs!: QueryList<ElementRef>;

  user$: Observable<User> = this.activatedRoute.params.pipe(
    switchMap(params => {
      if (params['id']) {
        return this.userService.get(params['id'])
      }
      return of(new User())
    })
    );

  faCircleChevronDown = faCircleChevronDown;
  faSquareCaretDown = faSquareCaretDown;

  clicked: boolean = false;

  usernameClass: string = '';
  emailClass: string = '';
  roleClass: string = '';
  coinsClass: string = '';
  selectedElement: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    // this.usernameFocusToggler('clicked');
  }

  ngAfterViewInit() {
    // console.log('inputRef: ', this.inputs);

    // const inputElements: any[] = [];

    this.inputs.changes.subscribe(sub =>{
      // sub.toArray().forEach((element: any) => inputElements.push(element.nativeElement))
      // console.log(inputElements);
      sub.toArray().forEach((element: any) => {
        console.log(element.nativeElement);
        setTimeout(()=>{

          if (element.nativeElement.value != '') {
            switch(element.nativeElement.name) {
              case 'username': {
                this.usernameClass = 'focused';
                break;
              }
              case 'email': {
                this.emailClass = 'focused';
                break;
              }
              case 'role': {
                this.roleClass = 'focused';
                break;
              }
            }
          }

          // const name: string = element.nativeElement.name;
          // const className: string = `${name}Class`;

          // if(element.nativeElement.value != '') {
          //   this.usernameClass = 'focused';
          //   this.emailClass = 'focused';
          // }
          // console.log(element.nativeElement.value);
          // console.log(element.nativeElement.name);
        },1)

      })

    });


    // setTimeout(()=>{


    //   // inputElements.forEach(e=>console.log('ggg',e));
    // },1400
    // )


    // console.log(this.inputs.get(0));

    // for( let oneInput in this.inputs) {
    //   console.log(oneInput);
    // }

    // console.log(this.inputs);

    // this.inputs.toArray().forEach(ref => {
    //   console.log(ref.nativeElement)
    // })

    // this.inputs.toArray().forEach(divRef => console.log(divRef));
  }

  focusToggler(event: Event, className: string): string {
    event.type == 'focus' ? className= 'focused' : className='';

    if(event) {
      this.selectedElement = event.target;
    } else {
      this.selectedElement = null;
    }

    if (this.selectedElement.value != '') {
      className= 'focused';
    }
    return className;
  }

  usernameFocusToggler(event: Event): void {
    this.usernameClass = this.focusToggler(event, this.usernameClass)
  }

  emailFocusToggler(event: Event): void {
    this.emailClass = this.focusToggler(event, this.emailClass)
  }

  roleFocusToggler(event: Event): void {
    this.roleClass = this.focusToggler(event, this.roleClass)
  }

  coinsFocusToggler(event: Event): void {
    this.coinsClass = this.focusToggler(event, this.coinsClass)
  }

  onUpdate(form: NgForm, user: User): void {
    this.clicked = true;
    if (user.id === 0) {
      this.userService.create(form.value).subscribe(
        () => this.router.navigate(['user']),
        err => console.error(err)
      );
    } else {
      this.userService.update(user).subscribe(
        () => this.router.navigate(['user']),
        err => console.error(err)
      );
    }
  }

}
