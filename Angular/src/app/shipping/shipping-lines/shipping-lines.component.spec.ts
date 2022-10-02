import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShippingLinesComponent } from './shipping-lines.component';

describe('BillingLinesComponent', () => {
  let component: ShippingLinesComponent;
  let fixture: ComponentFixture<ShippingLinesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShippingLinesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShippingLinesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
