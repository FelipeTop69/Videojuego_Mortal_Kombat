import { Component, inject, OnInit } from '@angular/core';
import { PizzaService } from '../../../Services/Entities/pizza.service';
import { Router, RouterLink } from '@angular/router';
import { PizzaMod } from '../../../Models/PizzaMod.model';
import Swal from 'sweetalert2';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { BaseTableComponent } from '../../../Components/Base/base-table/base-table.component';

@Component({
  selector: 'app-indice-admin',
  imports: [MatCardModule, BaseTableComponent, MatButtonModule, MatIconModule, RouterLink],
  templateUrl: './indice-admin.component.html',
  styleUrl: './indice-admin.component.css'
})
export class IndiceAdminComponent implements OnInit {

  pizzaService = inject(PizzaService);
  router = inject(Router)

  pizzaData : PizzaMod[] = [];
  columnasMostrar : string[] = [
    'N°', 'nombre', 'precio' 
  ];

  ngOnInit(): void {
    this.cargarPizzas();
  }

  cargarPizzas(): void {
    this.pizzaService.getAll().subscribe({
      next: (data) => {
        this.pizzaData = data;
        console.log(data);
      },
      error: (err) => {
        console.log('Error al cargar los datos:', err);
        const mensajeCompleto = err?.error?.message || 'Ocurrio un error inesperado.';
        const mensaje = mensajeCompleto.split(':')[1]?.trim() || mensajeCompleto;
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: mensaje,
          confirmButtonText: 'Aceptar'
        });
      }
    });
  }

    eliminarPizza(pizza: PizzaMod): void {
      console.log("Eliminar")
    }

  editarPizza(pizza: PizzaMod): void {
    console.log("Editar")
  }

}
