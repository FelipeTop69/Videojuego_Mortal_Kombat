namespace Entity.DTOs.Pedido
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string ClienteNombre { get; set; } = string.Empty;
        public string PizzaNombre { get; set; } = string.Empty;
    }
}
