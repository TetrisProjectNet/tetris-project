import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopItemEditorComponent } from './shop-item-editor.component';

describe('ShopItemEditorComponent', () => {
  let component: ShopItemEditorComponent;
  let fixture: ComponentFixture<ShopItemEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShopItemEditorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShopItemEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
