namespace Entity.DTOs.Ronda
{
    public class RondaHistorialDTO
    {
        public int Ronda { get; set; }
        public string Habilidad { get; set; } = string.Empty;
        public string GanadorNombre { get; set; } = string.Empty;
        public List<string> CartasJugadas { get; set; } = new();
    }
}
