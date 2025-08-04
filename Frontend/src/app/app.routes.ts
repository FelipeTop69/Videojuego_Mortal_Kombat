import { Routes } from '@angular/router';
import { LandingComponent } from './Views/Landing/landing.component';
import { RegistroJugadoresComponent } from './Views/Register/registro-jugadores.component';

export const routes: Routes = [
  {path: '', redirectTo: 'inicio',pathMatch: 'full'},
  {path: 'inicio', component: LandingComponent,
    data: {
      background: 'url(/img/fondos/Inicio.png)',
      title: 'Inicio'
    }
  },
  { path: 'registro',component: RegistroJugadoresComponent,
    data: {
      background: 'url(/img/fondos/Registro.png)',
      title: 'Registro de Jugadores'
    }
  },
  {
    path: '**',
    redirectTo: 'inicio'
  }
];
