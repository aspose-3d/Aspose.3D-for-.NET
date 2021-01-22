import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConvertingComponent } from './converting.component';

describe('ConvertingComponent', () => {
  let component: ConvertingComponent;
  let fixture: ComponentFixture<ConvertingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConvertingComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConvertingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
