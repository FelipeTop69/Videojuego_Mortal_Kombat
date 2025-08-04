import { Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RegisterDialogComponent } from '../../Components/Registrar_Jugador/register-dialog.component';
import { JugadorListado } from '../../Models/Jugador.model';
import { JugadorListComponent } from "../../Components/Jugador/JugadorListado/jugador-list.component";
import { CommonModule } from '@angular/common';
import { JugadorService } from '../../Services/jugador.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registro-jugadores',
  templateUrl: './registro-jugadores.component.html',
  styleUrls: ['./registro-jugadores.component.css'],
  imports: [JugadorListComponent, CommonModule]
})
export class RegistroJugadoresComponent implements OnInit{
  jugadores : JugadorListado[] = [];

  constructor(
    private dialog: MatDialog,
  ) { }

  private router = inject(Router)
  private jugadorService = inject(JugadorService)

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
}
