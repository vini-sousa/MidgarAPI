import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { Observable, take } from 'rxjs';

@Injectable()
export class LoteService {

  baseURL = 'https://localhost:7204/api/lotes';
  
  constructor(private http: HttpClient) { }

  public getLotesByEventId(eventId: number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseURL}/${eventId}`).pipe(take(1));
  }

  public saveLote(eventId: number, lotes: Lote[]): Observable<Lote[]> {
    return this.http.put<Lote[]>(`${this.baseURL}/${eventId}`, lotes).pipe(take(1));
  }

  public deleteLote(eventId: number, loteId: number): Observable<any> {
    return this.http.delete<any>(`${this.baseURL}/${eventId}/${loteId}`).pipe(take(1));
  }
}