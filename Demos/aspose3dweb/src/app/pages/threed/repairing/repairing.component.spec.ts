import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RepairingComponent } from './repairing.component';

describe('SaveComponent', () => {
  let component: RepairingComponent;
  let fixture: ComponentFixture<RepairingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RepairingComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RepairingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
