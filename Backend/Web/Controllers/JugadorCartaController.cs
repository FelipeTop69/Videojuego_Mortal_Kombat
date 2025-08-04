using AutoMapper;
using Entity.Context;
using Entity.DTOs.JugadorCarta;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilities.Enums;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JugadorCartaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public JugadorCartaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("by-jugador/{jugadorId}")]
        public async Task<ActionResult<IEnumerable<JugadorCartaDTO>>> GetCartasPorJugador(int jugadorId)
        {
            var cartas = await _context.JugadorCarta
                .Include(jc => jc.Carta)
                .Where(jc => jc.JugadorId == jugadorId)
                .ToListAsync();

            return Ok(_mapper.Map<List<JugadorCartaDTO>>(cartas));
        }

        [HttpPost("asignar-cartas")]
        public async Task<IActionResult> AsignarCartas()
        {
            var yaAsignadas = await _context.JugadorCarta.AnyAsync();
            if (yaAsignadas)
                return BadRequest("Ya se asignaron cartas. Reinicia el juego para volver a asignar.");

            var jugadores = await _context.Jugador.ToListAsync();
            var cartasDisponibles = await _context.Carta.Where(c => c.Disponible).ToListAsync();

            if (cartasDisponibles.Count < jugadores.Count * 8)
                return BadRequest("No hay suficientes cartas disponibles.");

            var jugadorCartas = new List<JugadorCarta>();

            foreach (var jugador in jugadores)
            {
                var cartasJugador = cartasDisponibles.OrderBy(_ => Guid.NewGuid()).Take(8).ToList();
                cartasDisponibles.RemoveAll(c => cartasJugador.Contains(c));

                foreach (var carta in cartasJugador)
                {
                    carta.Disponible = false;
                    jugadorCartas.Add(new JugadorCarta
                    {
                        JugadorId = jugador.Id,
                        CartaId = carta.Id,
                        Estado = EstadoCartaJugador.EnMano
                    });
                }
            }

            _context.JugadorCarta.AddRange(jugadorCartas);
            await _context.SaveChangesAsync();

            return Ok("Cartas asignadas correctamente.");
        }

        [HttpPut("jugar-carta")]
        public async Task<IActionResult> JugarCarta([FromBody] JugarCartaDTO dto)
        {
            var carta = await _context.JugadorCarta
                .FirstOrDefaultAsync(jc => jc.JugadorId == dto.JugadorId && jc.CartaId == dto.CartaId);

            if (carta == null) return NotFound("Carta no encontrada.");

            if (carta.Estado != EstadoCartaJugador.EnMano)
                return BadRequest("Esta carta ya fue jugada o no está disponible.");

            carta.Estado = EstadoCartaJugador.Jugada;
            carta.RondaJugado = dto.Ronda;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("reset")]
        public async Task<IActionResult> ResetJugadorCarta()
        {
            _context.JugadorCarta.RemoveRange(_context.JugadorCarta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
