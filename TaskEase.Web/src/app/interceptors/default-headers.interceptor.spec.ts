import { TestBed } from '@angular/core/testing';

import { DefaultHeadersInterceptor } from './default-headers.interceptor';

describe('DefaultHeadersInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      DefaultHeadersInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: DefaultHeadersInterceptor = TestBed.inject(DefaultHeadersInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
