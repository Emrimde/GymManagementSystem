import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientMainPage } from './client-main-page';

describe('ClientMainPage', () => {
  let component: ClientMainPage;
  let fixture: ComponentFixture<ClientMainPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientMainPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientMainPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
