namespace Entity.DTOs
{
    public class CartaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Resistencia { get; set; }
        public int Fuerza { get; set; }
        public int Salud { get; set; }
        public int Elemento { get; set; }
        public int Destreza { get; set; }
        public int GolpeFinal { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
    }
}