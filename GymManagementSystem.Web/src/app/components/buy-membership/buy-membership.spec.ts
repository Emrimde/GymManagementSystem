import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyMembership } from './buy-membership';

describe('BuyMembership', () => {
  let component: BuyMembership;
  let fixture: ComponentFixture<BuyMembership>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BuyMembership]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BuyMembership);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
