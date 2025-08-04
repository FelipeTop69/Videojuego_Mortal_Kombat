using Utilities.Enums;

namespace Entity.Models
{
    public class JugadorCarta
    {
        public int Id { get; set; }

        public int JugadorId { get; set; }
        public Jugador Jugador { get; set; } = new Jugador();

        public int CartaId { get; set; }
        public Carta Carta { get; set; } = new Carta();

        public EstadoCartaJugador Estado { get; set; } = new EstadoCartaJugador(); 

        public int? RondaJugado { get; set; }
    }

}