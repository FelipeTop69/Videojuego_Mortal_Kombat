namespace Entity.DTOs.Juego
{
    public class RondaResultadoDTO
    {
        public int Ronda { get; set; }
        public string Habilidad { get; set; } = string.Empty;
        public string Ganador { get; set; } = string.Empty;
        public List<CartaJugadaResumenDTO> CartasJugadas { get; set; } = [];
    }
}