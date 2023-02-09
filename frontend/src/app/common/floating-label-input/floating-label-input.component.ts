import { Component, ElementRef, Input, QueryList, ViewChild, ViewChildren } from '@angular/core';

@Component({
  selector: 'app-floating-label-input',
  templateUrl: './floating-label-input.component.html',
  styleUrls: ['./floating-label-input.component.scss']
})
export class FloatingLabelInputComponent {

  // @ViewChildren('inputRef') inputs!: QueryList<ElementRef>;
  @ViewChild('inputRef') input!: ElementRef;

  @Input() data: any;
  @Input() labelContent: string = '';
  @Input() name: string = '';
  @Input() type: string = '';

  inputClass: string = '';
  selectedElement: any;

  ngOnInit() {
    console.log('title: ', this.labelContent);
  }

  ngAfterViewInit() {
    setTimeout(() => {
      if (this.input.nativeElement.value != '') {
        console.log('asd');
        this.inputClass = 'focused';
      }
    }, 1)

    // this.input.nativeElement.subscribe((sub: any) => {
    //   // sub.toArray().forEach((element: any) => {
    //   console.log('sub ', sub);

    //   if (sub.value != '') {
    //     // switch (element.nativeElement.name) {
    //     //   case 'username': {
    //     //     this.usernameClass = 'focused';
    //     //     break;
    //     //   }
    //     //   case 'email': {
    //     //     this.emailClass = 'focused';
    //     //     break;
    //     //   }
    //     //   case 'role': {
    //     //     this.roleClass = 'focused';
    //     //     break;
    //     //   }
    //     //   case 'coins': {
    //     //     this.coinsClass = 'focused';
    //     //     break;
    //     //   }
    //     // }
    //   }

    //   // })
    // });
  }


  focusToggler(event: Event, className: string): string {
    event.type == 'focus' ? className = 'focused' : className = '';

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

  usernameFocusToggler(event: Event): void {
    this.inputClass = this.focusToggler(event, this.inputClass)
  }

}
