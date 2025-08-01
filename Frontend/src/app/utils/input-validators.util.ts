import { AbstractControl, ValidatorFn } from '@angular/forms';

/**
 * Bloquea cualquier intento de pegar texto no numérico en un input.
 */
export function blockNonNumericPaste(event: ClipboardEvent): void {
  const paste = event.clipboardData?.getData('text') || '';
  if (!/^\d+$/.test(paste)) {
    event.preventDefault();
  }
}

/**
 * Solo permite ingreso de caracteres numéricos mediante teclado.
 */
export function onlyNumberInput(event: KeyboardEvent): void {
  const charCode = event.key;
  if (!/^\d$/.test(charCode)) {
    event.preventDefault();
  }
}


/**
 * Solo permite contraseñas con el formato indicado.
 */
export function strongPassword(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const value = control.value;
        if (!value) return null;

        // Debe contener al menos: 1 mayúscula, 1 minúscula, 1 número y 1 caracter especial
        const hasUpperCase = /[A-Z]/.test(value);
        const hasLowerCase = /[a-z]/.test(value);
        const hasNumber = /[0-9]/.test(value);
        const hasSpecialChar = /[!@#$%^_&*+(),.?":{}|<>]/.test(value);

        const valid = hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar;

        return !valid ? { strongPassword: true } : null;
    };
}