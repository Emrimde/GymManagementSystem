import { TestBed } from '@angular/core/testing';

import { ScheduledClassService } from './scheduled-class-service';

describe('ScheduledClassService', () => {
  let service: ScheduledClassService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScheduledClassService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
