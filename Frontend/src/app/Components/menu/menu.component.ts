import { Component, inject } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../Services/Auth/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-menu',
  imports: [MatToolbarModule, MatButtonModule, MatIconModule, RouterLink],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent {
  authService = inject(AuthService)
  payload = this.authService.getTokenPayload();

  get username(): string {
    return this.payload.unique_name;
  }

  

  
  logout(){
    Swal.fire({
      icon: 'warning',
      title: 'Cerrar Sesión',
      text: '¿Deseas Cerrar Sesión?',
      showConfirmButton: true,
      showCancelButton: true,
      confirmButtonText: "Si, Cerrar",
      cancelButtonText: "Cancelar",
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6'
    }).then((result) => {
      if (result.isConfirmed){
        this.authService.logout()
      }
    })
  }
}
