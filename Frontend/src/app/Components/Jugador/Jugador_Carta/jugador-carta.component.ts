import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JugadorConCartas } from '../../../Models/JugadorCarta.model';

@Component({
  selector: 'app-jugador-carta',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './jugador-carta.component.html',
  styleUrls: ['./jugador-carta.component.css']
})
export class JugadorCartaComponent {
  @Input() jugador!: JugadorConCartas;
}
