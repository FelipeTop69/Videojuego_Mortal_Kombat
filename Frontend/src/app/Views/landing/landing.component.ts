import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { InfoDialogComponent } from '../../Components/Info/info-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  constructor(
    public dialog: MatDialog,
    private router: Router
  ) { }

  openInfo(): void {
    this.dialog.open(InfoDialogComponent, {
      width: '90%',
      maxWidth: '500px',
      disableClose: false,
      panelClass: 'custom-dialog-container'
    });
  }

  startGame(): void {
    this.router.navigate(['/registro']);
  }
}
