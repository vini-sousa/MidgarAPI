import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Events } from '../models/Events';
import { environment } from '@environments/environment';

@Injectable()
export class EventService {
  baseURL = environment.apiURL + 'api/events';
  
  constructor(private http: HttpClient) { }

  public getEvents(): Observable<Events[]> {
    return this.http.get<Events[]>(this.baseURL).pipe(take(1));
  }

  public getEventsByTheme(theme: string): Observable<Events[]> {
    return this.http.get<Events[]>(`${this.baseURL}/theme/${theme}`).pipe(take(1));
  }

  public getEventById(id: number): Observable<Events> {
    return this.http.get<Events>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public post(event: Events): Observable<Events> {
    return this.http.post<Events>(this.baseURL, event).pipe(take(1));
  }

  public put(event: Events): Observable<Events> {
    return this.http.put<Events>(`${this.baseURL}/${event.id}`, event).pipe(take(1));
  }

  public deleteEvent(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  postUpload(eventId: number, file: File): Observable<Events> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();

    formData.append('file', fileToUpload);

    return this.http.post<Events>(`${this.baseURL}/upload-image/${eventId}`, formData).pipe(take(1));
  }
}