using AutoMapper;
using Entity.Context;
using Entity.DTOs.Pedido;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PedidoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetAll()
        {
            var pedidos = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Pizza)
                .ToListAsync();

            var dto = _mapper.Map<List<PedidoDTO>>(pedidos);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> Create(PedidoCreateDTO dto)
        {
            var entity = _mapper.Map<Pedido>(dto);
            _context.Pedido.Add(entity);
            await _context.SaveChangesAsync();

            // Opcional: devolver el objeto creado ya con nombres mapeados
            var created = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Pizza)
                .FirstOrDefaultAsync(p => p.Id == entity.Id);

            return CreatedAtAction(nameof(GetAll), new { id = entity.Id }, _mapper.Map<PedidoDTO>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] string nuevoEstado)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null) return NotFound();

            pedido.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
