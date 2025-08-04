import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { JugadorCrear } from '../Models/Jugador.model';
import { GenericService } from './generic.service';
import { catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JugadorService extends GenericService<JugadorCrear, JugadorCrear> {
  constructor(http: HttpClient) {
    const baseURL = environment.apiURL + 'api/Jugador/registrar';
    super(http, baseURL);
  }

  override create(item: JugadorCrear): Observable<JugadorCrear> {
    return this.http.post<JugadorCrear>(this.baseUrl, item).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Ocurrió un error al registrar el jugador';

    if (error.error instanceof ErrorEvent) {
      // Error del lado del cliente
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Error del lado del servidor
      if (typeof error.error === 'object' && error.error !== null && 'message' in error.error) {
        errorMessage = (error.error as {message: string}).message;
      } else if (error.status === 400) {
        errorMessage = 'Datos inválidos enviados al servidor';
      } else if (error.status === 404) {
        errorMessage = 'Recurso no encontrado';
      } else if (error.status === 500) {
        errorMessage = 'Error interno del servidor';
      }
    }

    return throwError(() => new Error(errorMessage));
  }
}
