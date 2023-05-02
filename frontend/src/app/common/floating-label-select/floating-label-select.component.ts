import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { faSquareCaretDown } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-floating-label-select',
  templateUrl: './floating-label-select.component.html',
  styleUrls: ['./floating-label-select.component.scss']
})
export class FloatingLabelSelectComponent {

  @ViewChild('selectRef') select!: ElementRef;

  @Input() selectModel: any;
  @Output() selectModelChange = new EventEmitter<any>();
  @Input() labelContent: string = '';
  @Input() selectClass: string = '';
  @Input() name: string = '';
  @Input() required: boolean = true;
  @Input() isInitEmpty: boolean = false;
  @Input() doesContainEmpty: boolean = false;
  @Input() options: any;

  selectFocusClass: string = '';
  selectedElement: any;
  isValid: boolean = false;
  isFocused: boolean = false;
  isHovered: boolean = false;
  isValidationActive: boolean = false;

  faSquareCaretDown = faSquareCaretDown;

  ngOnInit() {
    if (this.isInitEmpty) {
      this.selectModel = "";
    }
  }

  ngAfterViewInit() {
    setTimeout(() => {
      if (this.select.nativeElement.value != '') {
        this.selectFocusClass = 'focused';
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

  selectFocusToggler(event: Event): void {
    if (event.type == 'blur') {
      this.isValidationActive = true;
    }
    this.isFocused = !this.isFocused;
    this.selectFocusClass = this.focusToggler(event, this.selectFocusClass)
  }

}
