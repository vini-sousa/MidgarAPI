import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Events } from '../models/Events';

@Injectable()
export class EventService {
  baseURL = 'https://localhost:7204/api/events';
  
  constructor(private http: HttpClient) { }

  public getEvents(): Observable<Events[]> {
    return this.http.get<Events[]>(this.baseURL);
  }

  public getEventsByTheme(theme: string): Observable<Events[]> {
    return this.http.get<Events[]>(`${this.baseURL}/${theme}/theme`);
  }

  public getEventById(id: number): Observable<Events[]> {
    return this.http.get<Events[]>(`{this.baseURL}/${id}`);
  }
}