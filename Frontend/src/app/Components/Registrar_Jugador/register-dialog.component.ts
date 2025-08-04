import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { JugadorCrear } from '../../Models/Jugador.model';
import { JugadorService } from '../../Services/jugador.service';
import { AvataresService } from '../../Services/avatares.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register-dialog',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class RegisterDialogComponent {
  playerName: string = '';
  isNameValid: boolean = false;
  isSubmitting: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<RegisterDialogComponent>,
    private jugadorService: JugadorService,
    private avataresService: AvataresService
  ) { }

  validateName(): void {
    const length = this.playerName.trim().length;
    this.isNameValid = length >= 3 && length <= 15;
  }

  async onSubmit(): Promise<void> {
    if (!this.isNameValid || this.isSubmitting) return;

    this.isSubmitting = true;
    const nombre = this.playerName.trim();

    try {
      this.avataresService.obtenerAvatarDisponible().subscribe({
        next: (avatar) => {
          if (!avatar) {
            Swal.fire('Ateción', 'Limite de 7 Jugadores Alcanzado', 'info');
            this.isSubmitting = false;
            return;
          }

          const jugador: JugadorCrear = { nombre, avatar };

          this.jugadorService.create(jugador).subscribe({
            next: (jugadorRegistrado: JugadorCrear) => {
              this.avataresService.marcarAvatarComoUsado(jugadorRegistrado.avatar);
              Swal.fire('¡Éxito!', `Jugador ${jugadorRegistrado.nombre} registrado`, 'success');
              this.dialogRef.close(jugadorRegistrado);
            },
            error: (err: Error) => {
              this.isSubmitting = false;
              Swal.fire('Error', err.message, 'error');
            }
          });
        },
        error: (error) => {
          this.isSubmitting = false;
          Swal.fire('Error', error.message, 'error');
        }
      });
    } catch (error: any) {
      this.isSubmitting = false;
      Swal.fire('Error', error.message || 'Error desconocido', 'error');
    }
  }

  private handleError(error: any): void {
    console.error('Error al registrar jugador:', error);

    if (error?.error?.includes("No se pueden registrar más de 7 jugadores")) {
      Swal.fire('Límite alcanzado', error.error, 'error');
    } else if (error?.error?.includes("El nombre del jugador ya existe")) {
      Swal.fire('Nombre duplicado', error.error, 'error');
    } else {
      const mensaje = error?.error || 'Ocurrió un error al registrar el jugador';
      Swal.fire('Error', mensaje, 'error');
    }
  }

  onCancel(): void {
    if (!this.isSubmitting) {
      this.dialogRef.close();
    }
  }
}
