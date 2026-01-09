import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPersonalBooking } from './add-personal-booking';

describe('AddPersonalBooking', () => {
  let component: AddPersonalBooking;
  let fixture: ComponentFixture<AddPersonalBooking>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddPersonalBooking]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPersonalBooking);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
