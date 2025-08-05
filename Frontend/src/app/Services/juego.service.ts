import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { RondaInicioDTO } from '../Models/juego.mode';

@Injectable({ providedIn: 'root' })
export class JuegoService {
  private baseUrl = environment.apiURL + 'api/Juego/'; // Asegúrate que coincida con tu API

  constructor(private http: HttpClient) {}

  iniciarRonda() {
    return this.http.post<RondaInicioDTO>(`${this.baseUrl}iniciar-ronda`, {}); // <- URL corregida
  }

  seleccionarHabilidad(habilidad: string) {
    return this.http.post(`${this.baseUrl}seleccionar-habilidad`, { habilidad });
  }
}
