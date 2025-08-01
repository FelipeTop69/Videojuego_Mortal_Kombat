import { Component, inject } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { MenuComponent } from './Components/menu/menu.component';
import { TokenMonitorService } from './Services/Auth/token-monitor.service';
import { filter } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MenuComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Frontend';

  private router = inject(Router);
  private tokenMonitor = inject(TokenMonitorService);

  get mostrarMenu(): boolean {
    const rutasOcultas = ['/Login',];
    return !rutasOcultas.some(r => this.router.url.startsWith(r));
  }

  ngOnInit(): void {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(event => {
        const url = (event as NavigationEnd).urlAfterRedirects;
        const rutasSinToken = ['/Login', '/Register'];

        const debeMonitorear = !rutasSinToken.some(ruta => url.startsWith(ruta));

        if (debeMonitorear) {
          this.tokenMonitor.startMonitoring();
        } else {
          this.tokenMonitor.stopMonitoring();
        }
      });
  }
}
