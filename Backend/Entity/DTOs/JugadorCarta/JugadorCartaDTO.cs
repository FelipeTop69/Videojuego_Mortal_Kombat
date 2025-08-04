using Utilities.Enums;

namespace Entity.DTOs.JugadorCarta
{
    public class JugadorCartaDTO
    {
        public int Id { get; set; }
        public int CartaId { get; set; }
        public string NombreCarta { get; set; } = string.Empty;
        public EstadoCartaJugador Estado { get; set; }
        public int? RondaJugado { get; set; }
    }
}