/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AgendaService } from './Agenda.service';

describe('Service: Evento', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AgendaService]
    });
  });

  it('should ...', inject([AgendaService], (service: AgendaService) => {
    expect(service).toBeTruthy();
  }));
});
