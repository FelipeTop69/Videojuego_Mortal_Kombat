import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';

export interface JwtPayload {
  unique_name: string; 
  exp: number;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly tokenKey = 'auth_token';
  private readonly baseUrl = `${environment.apiURL}api/Auth/`;

  constructor(private http: HttpClient, private router: Router) {}

  login(credentials: { username: string; password: string }) {
    return this.http.post<{ token: string }>(`${this.baseUrl}Login`, credentials);
  }


  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/Login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const { exp } = this.getTokenPayload();
    return exp * 1000 > Date.now(); // token aún válido
  }

  getTokenPayload(): JwtPayload {
    const token = this.getToken();
    return token ? jwtDecode<JwtPayload>(token) : { unique_name: '', exp: 0 };
  }
}