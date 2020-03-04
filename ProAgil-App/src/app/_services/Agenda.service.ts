import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Agenda } from '../_models/Agenda';

@Injectable({
  providedIn: 'root'
})
export class AgendaService {
    baseURL = 'http://localhost:5000/api/agenda';
constructor(private http: HttpClient) {}

  getAllEvento(): Observable<Agenda[]> {
    return this.http.get<Agenda[]>(this.baseURL);
  }

  getEventoByTema(tema: string): Observable<Agenda[]> {
    return this.http.get<Agenda[]>(`${this.baseURL}/getByTema/${tema}`);
  }

  getEventoById(id: number): Observable<Agenda[]> {
    return this.http.get<Agenda[]>(`${this.baseURL}/${id}`);
  }

  postEvento(evento: Agenda) {
    return this.http.post(this.baseURL, evento );
  }

  postUpload(file: File, name: string) {
    const fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, name);

    return this.http.post(`${this.baseURL}/upload`, formData);
  }

  putEvento(evento: Agenda) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento);
  }

  deleteEvento(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
