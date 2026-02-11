import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetClientPassword } from './reset-client-password';

describe('ResetClientPassword', () => {
  let component: ResetClientPassword;
  let fixture: ComponentFixture<ResetClientPassword>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResetClientPassword]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResetClientPassword);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
