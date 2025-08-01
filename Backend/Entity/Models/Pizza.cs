namespace Entity.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        public bool Active { get; set; } = true;
    }
}
