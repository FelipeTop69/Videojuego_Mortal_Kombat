using System.ComponentModel.DataAnnotations;

namespace Entity.Models
{
    public class Jugador
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public int OrdenTurno { get; set; }
        public int CantidadCartasGanadas { get; set; } = 0;
        public bool Active { get; set; } = true; 

        public ICollection<JugadorCarta> Cartas { get; set; } = [];
    }
}
