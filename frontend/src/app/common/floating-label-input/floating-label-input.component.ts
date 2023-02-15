import { Component, ElementRef, EventEmitter, Input, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { faTriangleExclamation } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-floating-label-input',
  templateUrl: './floating-label-input.component.html',
  styleUrls: ['./floating-label-input.component.scss']
})
export class FloatingLabelInputComponent {

  // @ViewChildren('inputRef') inputs!: QueryList<ElementRef>;
  @ViewChild('inputRef') input!: ElementRef;

  @Input() inputModel: any;
  @Output() inputModelChange = new EventEmitter<any>();
  @Output() isValidEvent = new EventEmitter<any>();

  // @Input() valid: any;
  // @Output() validChange = new EventEmitter<any>();

  @Input() labelContent: string = '';
  @Input() name: string = '';
  @Input() type: string = '';
  @Input() disabled: boolean = false;
  @Input() required: boolean = true;
  @Input() pattern: string | RegExp = '';

  inputClass: string = '';
  selectedElement: any;
  isValidOut: boolean = false;

  faTriangleExclamation = faTriangleExclamation;

  ngOnInit() {
  }

  ngAfterViewInit() {
    // console.log('valid: ', this.input.nativeElement.valid);
    setTimeout(() => {
      if (this.input.nativeElement.value != '') {
        this.inputClass = 'focused';
      }
    })
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

  validate(isValidIn: boolean | null) {
    // if (!isValidIn) {
    //   this.isValidOut = false;
    // } else {
    //   this.isValidOut = isValidIn;
    // }
    !isValidIn ? this.isValidOut = false : this.isValidOut = isValidIn;
    console.log('childParam: ', this.isValidOut);
    this.isValidEvent.emit(this.isValidOut);
  }

}
