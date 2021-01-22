import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewerdownComponent } from './viewerdown.component';

describe('ViewerdownComponent', () => {
  let component: ViewerdownComponent;
  let fixture: ComponentFixture<ViewerdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ViewerdownComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewerdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
