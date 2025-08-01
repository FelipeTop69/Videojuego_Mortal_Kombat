using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.Pedido
{
    public class PedidoUpdateEstadoDTO
    {
        public int PedidoId { get; set; }
        public string NuevoEstado { get; set; } = "Entregado";
    }
}
