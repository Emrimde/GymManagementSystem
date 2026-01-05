import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientPersonalTraining } from './client-personal-training';

describe('ClientPersonalTraining', () => {
  let component: ClientPersonalTraining;
  let fixture: ComponentFixture<ClientPersonalTraining>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientPersonalTraining]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientPersonalTraining);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
