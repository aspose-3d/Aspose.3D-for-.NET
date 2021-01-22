import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ThreedComponent } from './threed.component';

describe('ThreedComponent', () => {
  let component: ThreedComponent;
  let fixture: ComponentFixture<ThreedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ThreedComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ThreedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
