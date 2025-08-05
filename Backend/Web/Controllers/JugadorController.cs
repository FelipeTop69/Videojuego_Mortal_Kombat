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

        //EN USO
        [HttpPost("registrar")]
        public async Task<ActionResult<JugadorDTO>> CrearJugador(CrearJugadorDTO dto)
        {
            // 1. Validaciones iniciales
            if (string.IsNullOrWhiteSpace(dto?.Nombre))
                return BadRequest(new { message = "El nombre no puede estar vacío." });

            dto.Nombre = dto.Nombre.Trim();
            if (dto.Nombre.Length < 3 || dto.Nombre.Length > 15)
                return BadRequest(new { message = "El nombre debe tener entre 3 y 15 caracteres." });

            // 2. Iniciar transacción
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 3. Validar límite de jugadores (DENTRO de la transacción)
                var totalJugadores = await _context.Jugador.CountAsync();
                if (totalJugadores >= 7)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { message = "No se pueden registrar más de 7 jugadores." });
                }

                // 4. Validar nombre único (case-insensitive)
                var nombreNormalizado = dto.Nombre.ToLower();
                bool nombreRepetido = await _context.Jugador
                    .AnyAsync(j => j.Nombre.ToLower() == nombreNormalizado);

                if (nombreRepetido)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { message = "El nombre ya está en uso." });
                }

                // 6. Crear jugador
                var jugador = new Jugador
                {
                    Nombre = dto.Nombre,
                    OrdenTurno = totalJugadores + 1,
                    Avatar = dto.Avatar,
                    CantidadCartasGanadas = 0,
                    Active = true // ¡Asegurar que esté activo!
                };

                _context.Jugador.Add(jugador);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction(
                    nameof(GetJugador),
                    new { id = jugador.Id },
                    _mapper.Map<JugadorDTO>(jugador)
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error al registrar jugador: {ex}");
                return StatusCode(500, new { message = "Error interno al registrar jugador." });
            }
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
