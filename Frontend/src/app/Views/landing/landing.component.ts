import { Component, inject, OnInit } from '@angular/core';
import { Test } from '../../Models/Test.model';
import { TestConnectionService } from '../../Services/test-connection.service';

@Component({
  selector: 'app-landing',
  imports: [],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css'
})
export class LandingComponent implements OnInit {
  testService = inject(TestConnectionService);

  dataTests: Test[] = [];

  ngOnInit(): void {
    this.testService.getAll().subscribe({
      next: (data) => {
        this.dataTests = data;
        console.log('Data:', this.dataTests);
      },
      error: (err) => {
        console.error('Error al obtener usuarios:', err);
      }
    });
  }
}
