import { TestBed } from '@angular/core/testing';

import { TrainerServiceClient } from './trainer-service-client';

describe('TrainerServiceClient', () => {
  let service: TrainerServiceClient;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainerServiceClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
