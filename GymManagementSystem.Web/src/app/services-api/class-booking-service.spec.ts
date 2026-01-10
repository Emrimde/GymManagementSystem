import { TestBed } from '@angular/core/testing';

import { ClassBookingService } from './class-booking-service';

describe('ClassBookingService', () => {
  let service: ClassBookingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClassBookingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
