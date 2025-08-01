import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';


export const authGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const token = authService.getToken();
  const valid = authService.isAuthenticated();

  console.log('[GUARD] Token:', token);
  console.log('[GUARD] Autenticado:', valid);

  if (!valid) {
    router.navigate(['/Login']);
    return false;
  }

  return true;
};
