import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable, catchError, throwError } from 'rxjs';
import { JugadorConCartas } from '../Models/JugadorCarta.model';


@Injectable({
  providedIn: 'root'
})
export class JugadorCartaService {
  private baseUrl = environment.apiURL + 'api/JugadorCarta/';

  constructor(private http: HttpClient) {}

  asignarCartas(): Observable<string> {
    return this.http.post(
      `${this.baseUrl}asignar-cartas`,
      {},
      { responseType: 'text' }
    ).pipe(
      catchError(error => {
        const errorMsg = error.error?.message || error.message || 'Error al asignar cartas';
        return throwError(() => new Error(errorMsg));
      })
    );
  }

  getJugadoresConCartasActivas(): Observable<JugadorConCartas[]> {
    return this.http.get<JugadorConCartas[]>(`${this.baseUrl}jugadores-con-cartas-activas`);
  }
}
