import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientZone } from './client-zone';

describe('ClientZone', () => {
  let component: ClientZone;
  let fixture: ComponentFixture<ClientZone>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientZone]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientZone);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
