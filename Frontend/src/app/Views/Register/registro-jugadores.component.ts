import { Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RegisterDialogComponent } from '../../Components/Registrar_Jugador/register-dialog.component';
import { JugadorListado } from '../../Models/Jugador.model';
import { JugadorListComponent } from "../../Components/Jugador/JugadorListado/jugador-list.component";
import { CommonModule } from '@angular/common';
import { JugadorService } from '../../Services/jugador.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { JugadorCartaService } from '../../Services/jugador-carta.service';
import { JuegoService } from '../../Services/juego.service';

@Component({
  selector: 'app-registro-jugadores',
  templateUrl: './registro-jugadores.component.html',
  styleUrls: ['./registro-jugadores.component.css'],
  imports: [JugadorListComponent, CommonModule]
})
export class RegistroJugadoresComponent implements OnInit {
  jugadores: JugadorListado[] = [];

  constructor(
    private dialog: MatDialog,
  ) { }

  private router = inject(Router)
  private jugadorService = inject(JugadorService)
  private jugadorCartaService = inject(JugadorCartaService)
  private juegoService = inject(JuegoService)

  ngOnInit(): void {
    this.obtenerJugadores();
  }

  goBack(): void {
    this.router.navigate(['/inicio']);
  }

  openRegisterDialog(): void {
    const dialogRef = this.dialog.open(RegisterDialogComponent, {
      width: '90%',
      maxWidth: '450px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.obtenerJugadores();
      }
    });
  }

  obtenerJugadores(): void {
    this.jugadorService.getAll().subscribe(jugadores => {
      this.jugadores = jugadores;
    });
  }

  iniciarPartida(): void {
    if (this.jugadores.length < 2) {
      Swal.fire('Atención', 'Se necesitan al menos 2 jugadores para iniciar la partida.', 'warning');
      return;
    }

    this.jugadorCartaService.asignarCartas().subscribe({
      next: () => {
        this.juegoService.iniciarRonda().subscribe({
          next: (ronda) => {
            console.log('Ronda iniciada correctamente:', ronda);
            // Redirige al componente principal del juego
            this.router.navigate(['/juego']);
          },
          error: (error) => {
            console.error('Error al iniciar la ronda', error);
            alert('No se pudo iniciar la ronda. Asegúrate de haber registrado al menos dos jugadores.');
          }
        });
      },
      error: (err) => {
        Swal.fire('Error', err.message, 'error');
      }
    });
  }
}
