import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppExplicationComponent } from './app-explication.component';

describe('AppExplicationComponent', () => {
  let component: AppExplicationComponent;
  let fixture: ComponentFixture<AppExplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AppExplicationComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppExplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
