namespace Entity.Models
{
    public class Ronda
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string HabilidadSeleccionada { get; set; } = string.Empty;
        public int JugadorQueSeleccionaHabilidadId { get; set; }
        public DateTime Fecha { get; set; }
    }

}