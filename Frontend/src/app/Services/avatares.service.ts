import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { Observable, catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AvataresService {
  private baseUrl = environment.apiURL + 'api/Jugador';
  private avataresDisponibles: string[] = [
    'img/avatares/avatar_1.png',
    'img/avatares/avatar_2.png',
    'img/avatares/avatar_3.png',
    'img/avatares/avatar_4.png',
    'img/avatares/avatar_5.png',
    'img/avatares/avatar_6.png',
    'img/avatares/avatar_7.png'
  ];
  private avataresUsados: Set<string> = new Set();

  constructor(private http: HttpClient) {
    this.cargarAvataresUsados();
  }

  private cargarAvataresUsados(): void {
    this.obtenerAvataresUsadosBackend().subscribe({
      next: (avatares) => {
        avatares.forEach(avatar => this.avataresUsados.add(avatar));
      },
      error: (err) => {
        console.error('Error al cargar avatares usados:', err);
      }
    });
  }

  private obtenerAvataresUsadosBackend(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/avatares-usados`).pipe(
      catchError(() => of([] as string[]))
    );
  }

  obtenerAvatarDisponible(): Observable<string | null> {
    const disponiblesLocal = this.avataresDisponibles.filter(a => !this.avataresUsados.has(a));

    if (disponiblesLocal.length > 0) {
      const indice = Math.floor(Math.random() * disponiblesLocal.length);
      return of(disponiblesLocal[indice]);
    }

    return this.http.get<string[]>(`${this.baseUrl}/avatares-disponibles`).pipe(
      map(avatares => {
        if (!avatares || avatares.length === 0) return null;
        const indice = Math.floor(Math.random() * avatares.length);
        return avatares[indice];
      }),
      catchError(() => of(null))
    );
  }

  marcarAvatarComoUsado(avatar: string): void {
    this.avataresUsados.add(avatar);
  }

  liberarAvatar(avatar: string): void {
    this.avataresUsados.delete(avatar);
  }

  limpiarAvataresUsados(): void {
    this.avataresUsados.clear();
  }
}
