import { TestBed } from '@angular/core/testing';

import { PersonalBookingService } from './personal-booking-service';

describe('PersonalBookingService', () => {
  let service: PersonalBookingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PersonalBookingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
