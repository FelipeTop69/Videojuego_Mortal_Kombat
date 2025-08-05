import { Component, EventEmitter, Output, inject } from '@angular/core';
import { JuegoService } from '../../Services/juego.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-habilidad-selector',
  standalone: true,
  templateUrl: './habilidad-selector.component.html',
  styleUrls: ['./habilidad-selector.component.css'],
  imports: [CommonModule]
})
export class HabilidadSelectorComponent {
  private juegoService = inject(JuegoService);
  habilidades = [
    { clave: 'resistencia', nombre: 'Resistencia' },
    { clave: 'fuerza', nombre: 'Fuerza' },
    { clave: 'salud', nombre: 'Salud' },
    { clave: 'elemento', nombre: 'Elemento' },
    { clave: 'destreza', nombre: 'Destreza' },
    { clave: 'golpeFinal', nombre: 'Golpe Final' }
  ];

  @Output() habilidadConfirmada = new EventEmitter<string>();

  seleccionarHabilidad(habilidad: string): void {
    this.juegoService.seleccionarHabilidad(habilidad).subscribe({
      next: () => this.habilidadConfirmada.emit(habilidad),
      error: (err) => {
        console.error('Error:', err);
        alert('Error al seleccionar habilidad');
      }
    });
  }
}
