import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {
    baseURL = 'http://localhost:5000/api/agenda';
constructor(private http: HttpClient) {}

  getAllEvento(): Observable<User[]> {
    return this.http.get<User[]>(this.baseURL);
  }

  getEventoByTema(tema: string): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseURL}/getByTema/${tema}`);
  }

  getEventoById(id: number): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseURL}/${id}`);
  }

  postEvento(evento: User) {
    return this.http.post(this.baseURL, evento );
  }

  postUpload(file: File, name: string) {
    const fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, name);

    return this.http.post(`${this.baseURL}/upload`, formData);
  }

  putEvento(evento: User) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento);
  }

  deleteEvento(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
