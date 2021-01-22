import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RepairComponent } from './repair.component';

describe('RepairComponent', () => {
  let component: RepairComponent;
  let fixture: ComponentFixture<RepairComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RepairComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RepairComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
