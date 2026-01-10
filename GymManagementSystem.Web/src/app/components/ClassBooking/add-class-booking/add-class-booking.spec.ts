import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddClassBooking } from './add-class-booking';

describe('AddClassBooking', () => {
  let component: AddClassBooking;
  let fixture: ComponentFixture<AddClassBooking>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddClassBooking]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddClassBooking);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
