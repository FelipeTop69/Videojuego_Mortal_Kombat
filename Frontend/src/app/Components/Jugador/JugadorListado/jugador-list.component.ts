import { JugadorListado } from './../../../Models/Jugador.model';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-jugador-list',
  imports: [],
  templateUrl: './jugador-list.component.html',
  styleUrl: './jugador-list.component.css'
})
export class JugadorListComponent {
  @Input() jugador! : JugadorListado;
}
