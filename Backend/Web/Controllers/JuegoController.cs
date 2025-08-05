using Entity.Context;
using Entity.DTOs.Ronda;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilities.Enums;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JuegoController : ControllerBase
    {
        private readonly AppDbContext _context;

        private static readonly List<RondaHistorialDTO> _historial = new();

        public JuegoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("iniciar-ronda")]
        public async Task<ActionResult<RondaInicioDTO>> IniciarPrimeraRonda()
        {
            var jugadores = await _context.Jugador.ToListAsync();
            if (!jugadores.Any())
                return BadRequest("No hay jugadores registrados");

            bool rondaExiste = await _context.Ronda.AnyAsync(r => r.Numero == 1);
            if (rondaExiste)
                return BadRequest("La primera ronda ya ha sido iniciada");

            var jugadorAleatorio = jugadores.OrderBy(_ => Guid.NewGuid()).First();

            var habilidades = new[] { "Fuerza", "Resistencia", "Salud", "Elemento", "Destreza" };
            var habilidadElegida = habilidades.OrderBy(_ => Guid.NewGuid()).First();

            var ronda = new Ronda
            {
                Numero = 1,
                HabilidadSeleccionada = habilidadElegida,
                JugadorQueSeleccionaHabilidadId = jugadorAleatorio.Id,
                Fecha = DateTime.Now
            };

            _context.Ronda.Add(ronda);
            await _context.SaveChangesAsync();

            // Guardar también en historial en memoria
            var historialItem = new RondaHistorialDTO
            {
                NumeroRonda = 1,
                HabilidadSeleccionada = habilidadElegida,
                JugadorQueEligio = jugadorAleatorio.Nombre,
                CartasJugadas = new List<string>() // o new List<CartaJugadaDTO>() si usas la opción B
            };

            JuegoHistorial.EnRonda.Add(historialItem);

            var dto = new RondaInicioDTO
            {
                NumeroRonda = 1,
                Habilidad = habilidadElegida,
                JugadorQueEmpieza = jugadorAleatorio.Nombre
            };

            return Ok(dto);
        }



        [HttpPost("seleccionar-habilidad")]
        public async Task<IActionResult> SeleccionarHabilidad([FromBody] string habilidad)
        {
            if (string.IsNullOrWhiteSpace(habilidad))
                return BadRequest("La habilidad no puede estar vacía.");

            var habilidadesValidas = new[] { "Fuerza", "Resistencia", "Salud", "Elemento", "Destreza", "GolpeFinal" };

            if (!habilidadesValidas.Contains(habilidad))
                return BadRequest("Habilidad no válida.");

            var nuevaRonda = new Ronda
            {
                HabilidadSeleccionada = habilidad
            };

            _context.Ronda.Add(nuevaRonda);
            await _context.SaveChangesAsync();

            return Ok(new { ronda = nuevaRonda.Id, habilidad });
        }   

        [HttpPost("calcular-ganador-ronda")]
        public async Task<IActionResult> EvaluarRonda([FromBody] EvaluarRondaDTO dto)
        {
            if (_historial.Any(h => h.NumeroRonda == dto.Ronda))
                return BadRequest("Esta ronda ya fue evaluada.");

            var cartasJugadas = await _context.JugadorCarta
                .Include(jc => jc.Carta)
                .Include(jc => jc.Jugador)
                .Where(jc => jc.Estado == EstadoCartaJugador.Jugada && jc.RondaJugado == dto.Ronda)
                .ToListAsync();

            if (!cartasJugadas.Any())
                return BadRequest("No hay cartas jugadas en esta ronda.");

            var cartaGanadora = cartasJugadas
                .OrderByDescending(jc => GetValorPorHabilidad(jc.Carta, dto.Habilidad))
                .First();

            var ganador = cartaGanadora.Jugador;

            if (ganador == null)
                return NotFound("Ganador no encontrado.");

            foreach (var carta in cartasJugadas)
            {
                carta.Estado = EstadoCartaJugador.Ganada;
                // Ya no reasignamos la carta al ganador (solo sumamos el conteo)
            }

            ganador.CantidadCartasGanadas += cartasJugadas.Count;

            _historial.Add(new RondaHistorialDTO
            {
                NumeroRonda = dto.Ronda,
                HabilidadSeleccionada = dto.Habilidad,
                GanadorNombre = ganador.Nombre,
                CartasJugadas = cartasJugadas.Select(j => j.Carta.Nombre).ToList()
            });

            await _context.SaveChangesAsync();
            return Ok(new { ganador = ganador.Nombre });
        }

        [HttpGet("historial")]
        public IActionResult ObtenerHistorial()
        {
            return Ok(_historial);
        }

        [HttpDelete("reiniciar")]
        public async Task<IActionResult> ReiniciarJuego()
        {
            _historial.Clear();

            _context.JugadorCarta.RemoveRange(_context.JugadorCarta);
            _context.Jugador.RemoveRange(_context.Jugador);
            _context.Ronda.RemoveRange(_context.Ronda);

            var cartas = await _context.Carta.ToListAsync();
            foreach (var carta in cartas)
                carta.Disponible = true;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private static int GetValorPorHabilidad(Carta carta, string habilidad) =>
            habilidad switch
            {
                "Resistencia" => carta.Resistencia,
                "Fuerza" => carta.Fuerza,
                "Salud" => carta.Salud,
                "Elemento" => carta.Elemento,
                "Destreza" => carta.Destreza,
                "GolpeFinal" => carta.GolpeFinal,
                _ => 0
            };
    }
}
