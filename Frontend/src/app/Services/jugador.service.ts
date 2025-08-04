import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { GenericService } from './generic.service';
import { catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JugadorService extends GenericService<any> {
  constructor(http: HttpClient) {
    const baseURL = environment.apiURL + 'api/Jugador/';
    super(http, baseURL);
  }

  override create(item: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'registrar', item).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Ocurrió un error al registrar el jugador';

    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
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
