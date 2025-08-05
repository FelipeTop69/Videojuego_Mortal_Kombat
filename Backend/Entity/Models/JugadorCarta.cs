using Utilities.Enums;

namespace Entity.Models
{
    public class JugadorCarta
    {
        public int Id { get; set; }

        public int JugadorId { get; set; }
        public Jugador Jugador { get; set; } // ← ¡NO inicializar!

        public int CartaId { get; set; }
        public Carta Carta { get; set; }     // ← ¡NO inicializar!

        public bool Active { get; set; } = true; 

        public EstadoCartaJugador Estado { get; set; } = EstadoCartaJugador.EnMano;
        public int? RondaJugado { get; set; }
    }
}