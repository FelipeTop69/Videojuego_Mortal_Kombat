namespace Entity.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente"; // Pendiente, En Proceso, Entregado

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; } = null!;
    }
}
