namespace Entity.DTOs.JugadorCarta
{
    public class AsignarCartasGanadasDTO
    {
        public List<int> JugadorCartaIds { get; set; } = [];
        public int GanadorId { get; set; }
    }
}