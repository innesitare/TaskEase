import { TestBed } from '@angular/core/testing';

import { BoardTaskService } from './board-task.service';

describe('BoardTaskService', () => {
  let service: BoardTaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BoardTaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
