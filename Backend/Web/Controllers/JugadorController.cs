using AutoMapper;
using Entity.Context;
using Entity.DTOs.Jugador;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JugadorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public JugadorController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JugadorDTO>>> GetJugadores()
        {
            var jugadores = await _context.Jugador
                .OrderBy(j => j.OrdenTurno)
                .ToListAsync();

            return Ok(_mapper.Map<List<JugadorDTO>>(jugadores));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JugadorDTO>> GetJugador(int id)
        {
            var jugador = await _context.Jugador.FindAsync(id);
            if (jugador == null)
                return NotFound("Jugador no encontrado.");

            return Ok(_mapper.Map<JugadorDTO>(jugador));
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<JugadorDTO>> CrearJugador(CrearJugadorDTO dto)
        {
            var totalJugadores = await _context.Jugador.CountAsync();
            var nombreNormalizado = dto.Nombre.Trim().ToLower();

            if (totalJugadores >= 7)
            {
                return BadRequest(new { message = "No se pueden registrar más de 7 jugadores." });
            }

            bool nombreRepetido = await _context.Jugador
                .AnyAsync(j => j.Nombre.ToLower() == nombreNormalizado);

            if (nombreRepetido)
            {
                return BadRequest(new { message = "El nombre del jugador ya existe." });
            }

            int orden = await _context.Jugador.CountAsync() + 1;

            var jugador = new Jugador
            {
                Nombre = dto.Nombre.Trim(),
                OrdenTurno = orden,
                Avatar = dto.Avatar,
                CantidadCartasGanadas = 0
            };

            _context.Jugador.Add(jugador);
            await _context.SaveChangesAsync();

            var jugadorDTO = _mapper.Map<JugadorDTO>(jugador);
            return CreatedAtAction(nameof(GetJugador), new { id = jugador.Id }, jugadorDTO);
        }

        [HttpGet("avatares-usados")]
        public async Task<ActionResult<IEnumerable<string>>> GetAvataresUsados()
        {
            var avatares = await _context.Jugador
                .Select(j => j.Avatar)
                .Distinct()
                .ToListAsync();

            return Ok(avatares);
        }

        [HttpGet("avatares-disponibles")]
        public async Task<ActionResult<IEnumerable<string>>> GetAvataresDisponibles()
        {
            var avataresUsados = await _context.Jugador
                .Select(j => j.Avatar)
                .Distinct()
                .ToListAsync();

            var todosAvatares = new List<string>
    {
                "img/avatares/avatar_1.png",
                "img/avatares/avatar_2.png",
                "img/avatares/avatar_3.png",
                "img/avatares/avatar_4.png",
                "img/avatares/avatar_5.png",
                "img/avatares/avatar_6.png",
                "img/avatares/avatar_7.png"
            };

            var disponibles = todosAvatares.Except(avataresUsados).ToList();

            return Ok(disponibles);
        }

        [HttpDelete("reset")]
        public async Task<IActionResult> ResetJugadores()
        {
            var jugadores = await _context.Jugador.ToListAsync();

            if (!jugadores.Any())
                return NoContent();

            var jugadorIds = jugadores.Select(j => j.Id).ToList();

            var jugadorCartas = await _context.JugadorCarta
                .Where(jc => jugadorIds.Contains(jc.JugadorId))
                .ToListAsync();

            _context.JugadorCarta.RemoveRange(jugadorCartas);
            _context.Jugador.RemoveRange(jugadores);

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Jugadores y cartas eliminadas correctamente." });
        }
    }
}
