import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export abstract class GenericService<TWrite, TRead> {

    constructor(
    protected http: HttpClient,
    protected baseUrl: string
  ) { }

  getAll(): Observable<TRead[]> {
    return this.http.get<TRead[]>(`${this.baseUrl}`);
  }

  getById(id: number): Observable<TRead> {
    return this.http.get<TRead>(`${this.baseUrl}/${id}`);
  }

  create(item: TWrite): Observable<TWrite> {
    return this.http.post<TWrite>(`${this.baseUrl}`, item);
  }

  update(item: TWrite): Observable<TWrite> {
    return this.http.put<TWrite>(`${this.baseUrl}`, item);
  }

  deletePersitence(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

}