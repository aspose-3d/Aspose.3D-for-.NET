import { TestBed } from '@angular/core/testing';

import { ResourceENService } from './resource-en.service';

describe('ResourceENService', () => {
  let service: ResourceENService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResourceENService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
