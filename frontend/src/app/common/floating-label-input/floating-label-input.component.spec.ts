import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FloatingLabelInputComponent } from './floating-label-input.component';

describe('FloatingLabelInputComponent', () => {
  let component: FloatingLabelInputComponent;
  let fixture: ComponentFixture<FloatingLabelInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FloatingLabelInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FloatingLabelInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
