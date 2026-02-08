import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalTrainerDashboard } from './personal-trainer-dashboard';

describe('PersonalTrainerDashboard', () => {
  let component: PersonalTrainerDashboard;
  let fixture: ComponentFixture<PersonalTrainerDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonalTrainerDashboard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonalTrainerDashboard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
