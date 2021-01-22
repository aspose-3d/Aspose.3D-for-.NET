import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConvertDownloadComponent } from './convert-download.component';

describe('ConvertDownloadComponent', () => {
  let component: ConvertDownloadComponent;
  let fixture: ComponentFixture<ConvertDownloadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConvertDownloadComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConvertDownloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
