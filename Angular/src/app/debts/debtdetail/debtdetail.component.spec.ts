import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DebtdetailComponent } from './debtdetail.component';

describe('DebtdetailComponent', () => {
  let component: DebtdetailComponent;
  let fixture: ComponentFixture<DebtdetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DebtdetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DebtdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
