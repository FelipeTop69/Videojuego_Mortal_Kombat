import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { GenericService } from './generic.service';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JugadorCartaService extends GenericService<any> {

  constructor(http: HttpClient) {
    const baseURL = environment.apiURL + 'api/JugadorCarta/';
    super(http, baseURL);
  }
  asignarCartas(): Observable<{ message: string, totalAsignadas: number }> {
    return this.http.post<{ message: string, totalAsignadas: number }>(
      `${this.baseUrl}asignar-cartas`,
      {}
    ).pipe(
      catchError(error => {
        // Manejo centralizado de errores
        let errorMsg = 'Error al asignar cartas';
        if (error.error?.message) errorMsg = error.error.message;
        return throwError(() => new Error(errorMsg));
      })
    );
  }
}
