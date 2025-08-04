import { Component, inject, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [RouterOutlet]
})
export class AppComponent implements OnInit {
  private router = inject(Router);
  currentBackground = '';

  ngOnInit() {
    this.setupRouterEvents();
    this.updateBackground();
  }

  private setupRouterEvents() {
    this.router.events
      .pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd))
      .subscribe(() => {
        this.updateBackground();
      });
  }

  private updateBackground() {
    const route = this.getCurrentRoute();
    this.currentBackground = route?.data?.['background'];
  }

  private getCurrentRoute() {
    let route = this.router.routerState.snapshot.root;
    while (route.firstChild) {
      route = route.firstChild;
    }
    return route;
  }
}
