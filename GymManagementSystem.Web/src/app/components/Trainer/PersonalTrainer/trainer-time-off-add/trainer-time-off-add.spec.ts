import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerTimeOffAdd } from './trainer-time-off-add';

describe('TrainerTimeOffAdd', () => {
  let component: TrainerTimeOffAdd;
  let fixture: ComponentFixture<TrainerTimeOffAdd>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainerTimeOffAdd]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerTimeOffAdd);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
