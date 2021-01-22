import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavFooterComponent } from './nav-footer.component';

describe('NavFooterComponent', () => {
  let component: NavFooterComponent;
  let fixture: ComponentFixture<NavFooterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [NavFooterComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
