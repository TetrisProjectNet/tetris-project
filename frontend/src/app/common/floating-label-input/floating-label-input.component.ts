import { Component, ElementRef, EventEmitter, Input, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { faTriangleExclamation } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-floating-label-input',
  templateUrl: './floating-label-input.component.html',
  styleUrls: ['./floating-label-input.component.scss']
})
export class FloatingLabelInputComponent {

  @ViewChild('inputRef') input!: ElementRef;

  @Input() inputModel: any;
  @Output() inputModelChange = new EventEmitter<any>();
  @Input() labelContent: string = '';
  @Input() inputClass: string = '';
  @Input() name: string = '';
  @Input() type: string = '';
  @Input() disabled: boolean = false;
  @Input() required: boolean = true;
  @Input() pattern: string | RegExp = '';
  @Output() isValidEvent = new EventEmitter<any>();
  @Input() firstPassword: string = '';
  
  inputFocusClass: string = '';
  selectedElement: any;
  isValid: boolean = false;

  faTriangleExclamation = faTriangleExclamation;

  ngOnInit() {
  }

  ngAfterViewInit() {
    setTimeout(() => {
      if (this.input.nativeElement.value != '') {
        this.inputFocusClass = 'focused';
        if (this.input.nativeElement.value.match(this.input.nativeElement.pattern)) {
          this.isValid = true;
          this.isValidEvent.emit(this.isValid);
        }
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
    this.inputFocusClass = this.focusToggler(event, this.inputFocusClass)
  }

  validate(isValidIn: boolean | null) {
    !isValidIn ? this.isValid = false : this.isValid = isValidIn;
    this.isValidEvent.emit(this.isValid);
  }

}
