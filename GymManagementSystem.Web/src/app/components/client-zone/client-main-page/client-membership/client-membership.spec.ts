import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientMembership } from './client-membership';

describe('ClientMembership', () => {
  let component: ClientMembership;
  let fixture: ComponentFixture<ClientMembership>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientMembership]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientMembership);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
