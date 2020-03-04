/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserService } from './User.service';

describe('Service: Evento', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserService]
    });
  });

  it('should ...', inject([UserService], (service: UserService) => {
    expect(service).toBeTruthy();
  }));
});
