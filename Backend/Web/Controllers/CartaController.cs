using AutoMapper;
using Entity.Context;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CartaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartaDTO>>> GetCartas([FromQuery] bool disponibles = false)
        {
            var query = _context.Carta.AsQueryable();

            if (disponibles)
                query = query.Where(c => c.Disponible);

            var cartas = await query.ToListAsync();
            return Ok(_mapper.Map<List<CartaDTO>>(cartas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartaDTO>> GetCarta(int id)
        {
            var carta = await _context.Carta.FindAsync(id);
            if (carta == null)
                return NotFound();

            return Ok(_mapper.Map<CartaDTO>(carta));
        }

        [HttpPut("reset-disponibles")]
        public async Task<IActionResult> ResetDisponibles()
        {
            var cartas = await _context.Carta.ToListAsync();

            if (!cartas.Any())
                return NotFound("No hay cartas para actualizar.");

            foreach (var carta in cartas)
                carta.Disponible = true;

            await _context.SaveChangesAsync();

            return Ok(new { updatedCount = cartas.Count });
        }
    }
}
