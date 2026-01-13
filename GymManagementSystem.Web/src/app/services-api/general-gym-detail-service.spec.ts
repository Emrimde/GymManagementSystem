import { TestBed } from '@angular/core/testing';

import { GeneralGymDetailService } from './general-gym-detail-service';

describe('GeneralGymDetailService', () => {
  let service: GeneralGymDetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeneralGymDetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
