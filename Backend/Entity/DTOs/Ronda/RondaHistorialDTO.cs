namespace Entity.DTOs.Ronda
{
    public class RondaHistorialDTO
    {
        public int NumeroRonda { get; set; }
        public string HabilidadSeleccionada { get; set; } = string.Empty;
        public string JugadorQueEligio { get; set; } = string.Empty;
        public string GanadorNombre { get; set; } = string.Empty;
        public List<string> CartasJugadas { get; set; } = new();
    }
}
