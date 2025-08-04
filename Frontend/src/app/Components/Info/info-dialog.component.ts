import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-info-dialog',
  imports: [],
  templateUrl: './info-dialog.component.html',
  styleUrl: './info-dialog.component.css'
})
export class InfoDialogComponent {
  constructor(public dialogRef: MatDialogRef<InfoDialogComponent>) {}

  closeDialog(): void {
    this.dialogRef.close();
  }
}
