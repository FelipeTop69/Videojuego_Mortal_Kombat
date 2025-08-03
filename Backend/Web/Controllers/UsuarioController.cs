using AutoMapper;
using Entity.Context;
using Entity.DTOs;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _context.Usuario.Include(u => u.Rol).ToListAsync();
            return Ok(_mapper.Map<List<UsuarioDTO>>(usuarios));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var usuario = await _context.Usuario.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return NotFound();
            return Ok(_mapper.Map<UsuarioDTO>(usuario));
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioDTO dto)
        {
            var usuario = _mapper.Map<Usuario>(dto);
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, _mapper.Map<UsuarioDTO>(usuario));
        }
    }
}