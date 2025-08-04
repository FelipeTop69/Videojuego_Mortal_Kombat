import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RegisterDialogComponent } from '../../Components/Registrar_Jugador/register-dialog.component';
import { JugadorService } from '../../Services/jugador.service';
import { AvataresService } from '../../Services/avatares.service';

@Component({
  selector: 'app-registro-jugadores',
  templateUrl: './registro-jugadores.component.html',
  styleUrls: ['./registro-jugadores.component.css']
})
export class RegistroJugadoresComponent {
  constructor(
    private dialog: MatDialog,
    private jugadorService: JugadorService,
    private avataresService: AvataresService
  ) { }

  openRegisterDialog(): void {
    const dialogRef = this.dialog.open(RegisterDialogComponent, {
      width: '90%',
      maxWidth: '450px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Actualizar lista de jugadores si es necesario
      }
    });
  }


}
