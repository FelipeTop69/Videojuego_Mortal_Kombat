import { JuegoService } from './../../../Services/juego.service';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JugadorConCartas } from '../../../Models/JugadorCarta.model';
import { JugadorCartaService } from '../../../Services/jugador-carta.service';
import { JugadorCartaComponent } from "../../../Components/Jugador/Jugador_Carta/jugador-carta.component";
import { HabilidadSelectorComponent } from "../../../Components/SelecHabilidad/habilidad-selector.component";
import { RondaInicioDTO } from '../../../Models/juego.mode';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [CommonModule, JugadorCartaComponent, HabilidadSelectorComponent],
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {
  jugadores: JugadorConCartas[] = [];
  mostrarSelectorHabilidad: boolean = true;
  mostrarSelectorCarta: boolean = false;

  habilidadSeleccionada: string = '';

  rondaActual: RondaInicioDTO | null = null;

  constructor() { }

  private jugadorCartaService = inject(JugadorCartaService)
  private juegoService = inject(JuegoService)

  ngOnInit(): void {
    this.cargarRondaActual();
    this.obtenerJugadoresConCartas();
  }

    cargarRondaActual() {
    this.juegoService.iniciarRonda().subscribe({
      next: (ronda) => {
        this.rondaActual = ronda;
        this.mostrarSelectorHabilidad = ronda.Habilidad === null;
      },
      error: (err) => console.error('Error al cargar ronda', err)
    });
  }

  obtenerJugadoresConCartas(): void {
    this.jugadorCartaService.getJugadoresConCartasActivas().subscribe({
      next: (jugadores) => this.jugadores = jugadores,
      error: (err) => console.error('Error cargando jugadores', err)
    });
  }

  avanzarAFaseDeSeleccion() {
    this.mostrarSelectorHabilidad = false;
    this.mostrarSelectorCarta = true;
    if (this.rondaActual) {
      this.habilidadSeleccionada = this.rondaActual.Habilidad || 'Aún no seleccionada';
    }
  }

}
