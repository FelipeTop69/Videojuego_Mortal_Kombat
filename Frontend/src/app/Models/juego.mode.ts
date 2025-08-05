export interface RondaInicioDTO {
  NumeroRonda: number;
  Habilidad: string | null;
  JugadorQueEmpieza: string;
}

export interface HabilidadDTO {
  clave: string;
  nombre: string;
}
