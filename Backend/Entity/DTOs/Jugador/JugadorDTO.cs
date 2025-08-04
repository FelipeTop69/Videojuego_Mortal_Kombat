namespace Entity.DTOs.Jugador
{
    public class JugadorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public int OrdenTurno { get; set; }
    }
}