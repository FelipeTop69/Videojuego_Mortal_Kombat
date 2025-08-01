import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../Services/Auth/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, RouterLink, MatIconModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  
  hidePassword = true;

  loginForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', [
      Validators.required
    ]]
  });

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const { username, password } = this.loginForm.value;
    
    this.authService.login({ username: username!, password: password! }).subscribe({
      next: (res) => {
        this.authService.saveToken(res.token);
        this.router.navigate(['/']); // redirige al home o dashboard
      },
      error: () => {
        Swal.fire({
          icon: 'error',
          title: 'Oopss...',
          text: 'Credenciales Incorrectas',
          confirmButtonText: 'Aceptar'
        })
      }
    });
  }

  // loginWithGoogle(): void {
  //   const clientId = '680262904613-vohgqb9a3pp7tmnb8b72je2h1uiv0r0k.apps.googleusercontent.com';
  //   const redirectUri = 'http://localhost:4200/oauth/callback';
  //   const scope = 'openid profile email';
  //   const responseType = 'code';
  //   const url = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=${responseType}&scope=${scope}&access_type=offline`;

  //   window.location.href = url;
  // }

}