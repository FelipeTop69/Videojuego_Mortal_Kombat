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

        /// <summary>
        /// Retorna la lista de jugadores ordenados por turno.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JugadorDTO>>> GetJugadores()
        {
            var jugadores = await _context.Jugador
                .OrderBy(j => j.OrdenTurno)
                .ToListAsync();

            return Ok(_mapper.Map<List<JugadorDTO>>(jugadores));
        }

        /// <summary>
        /// Retorna los datos de un jugador por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<JugadorDTO>> GetJugador(int id)
        {
            var jugador = await _context.Jugador.FindAsync(id);
            if (jugador == null)
                return NotFound("Jugador no encontrado.");

            return Ok(_mapper.Map<JugadorDTO>(jugador));
        }

        /// <summary>
        /// Crea un nuevo jugador con nombre único.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<JugadorDTO>> CrearJugador(CrearJugadorDTO dto)
        {
            var totalJugadores = await _context.Jugador.CountAsync();
            var nombreNormalizado = dto.Nombre.Trim().ToLower();

            if (totalJugadores >= 7)
                return BadRequest("No se pueden registrar más de 7 jugadores.");

            bool nombreRepetido = await _context.Jugador
                .AnyAsync(j => j.Nombre.ToLower() == nombreNormalizado);

            if (nombreRepetido)
                return BadRequest("El nombre del jugador ya existe.");

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

        /// <summary>
        /// Elimina todos los jugadores y sus relaciones.
        /// </summary>
        [HttpDelete("reset")]
        public async Task<IActionResult> ResetJugadores()
        {
            var jugadores = await _context.Jugador.ToListAsync();

            if (!jugadores.Any())
                return NoContent();

            // También borra relaciones con cartas si hay navegación configurada
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
