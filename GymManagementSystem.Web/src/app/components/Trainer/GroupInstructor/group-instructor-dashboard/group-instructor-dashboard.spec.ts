import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupInstructorDashboard } from './group-instructor-dashboard';

describe('GroupInstructorDashboard', () => {
  let component: GroupInstructorDashboard;
  let fixture: ComponentFixture<GroupInstructorDashboard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GroupInstructorDashboard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GroupInstructorDashboard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
