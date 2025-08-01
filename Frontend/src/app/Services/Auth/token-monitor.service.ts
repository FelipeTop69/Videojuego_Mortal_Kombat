import { Injectable, inject } from '@angular/core';
import { AuthService } from './auth.service';
import Swal from 'sweetalert2';

@Injectable({ providedIn: 'root' })
export class TokenMonitorService {
  private authService = inject(AuthService);
  private warningThreshold = 30; // segundos antes de expirar
  private intervalId: any;
  private activityListenerAttached = false;
  private warned = false;

  startMonitoring(): void {
    if (!this.authService.isAuthenticated()) return;
    this.scheduleTokenCheck();
  }

  stopMonitoring(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
      this.intervalId = null;
    }
    this.warned = false;
  }


  private scheduleTokenCheck(): void {
    if (this.intervalId) clearInterval(this.intervalId);

    this.intervalId = setInterval(() => {
      const token = this.authService.getToken();
      if (!token) return;

      const { exp } = this.authService.getTokenPayload();
      const timeLeft = exp * 1000 - Date.now();

      if (timeLeft <= 0) {
        this.forceLogout();
      } else if (timeLeft <= this.warningThreshold * 1000 && !this.warned) {
        this.warned = true;
        this.warnBeforeLogout(Math.floor(timeLeft / 1000));
      }
    }, 5000);
  }

  private async warnBeforeLogout(secondsLeft: number): Promise<void> {
    const result = await Swal.fire({
      title: 'Tu sesión está por expirar',
      html: `Tu sesión expirará en <b>${secondsLeft}</b> segundos.`,
      icon: 'warning',
      showConfirmButton: false,
      timer: secondsLeft * 1000,
      timerProgressBar: true
    });
  
    // Esta condición se cumple solo si el modal se cerró por el temporizador
    if (result.dismiss === Swal.DismissReason.timer && this.warned) {
      this.forceLogout();
    }
  }



  private forceLogout(): void {
    this.authService.logout();
    Swal.fire({
      title: 'Sesión cerrada',
      text: 'Tu sesión ha expirado.',
      icon: 'info',
      timer: 3000
    });
  }
}
