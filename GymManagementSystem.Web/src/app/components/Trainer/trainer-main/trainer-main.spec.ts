import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainerMain } from './trainer-main';

describe('TrainerMain', () => {
  let component: TrainerMain;
  let fixture: ComponentFixture<TrainerMain>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainerMain]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainerMain);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
