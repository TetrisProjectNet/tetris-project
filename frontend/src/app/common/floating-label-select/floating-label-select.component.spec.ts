import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FloatingLabelSelectComponent } from './floating-label-select.component';

describe('FloatingLabelSelectComponent', () => {
  let component: FloatingLabelSelectComponent;
  let fixture: ComponentFixture<FloatingLabelSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FloatingLabelSelectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FloatingLabelSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
