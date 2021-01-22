import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeasurementdownComponent } from './measurementdown.component';

describe('MeasurementdownComponent', () => {
  let component: MeasurementdownComponent;
  let fixture: ComponentFixture<MeasurementdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MeasurementdownComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeasurementdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
