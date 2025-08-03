using AutoMapper;
using Entity.Context;
using Entity.DTOs;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PizzaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PizzaDTO>> GetAll()
        {
            var pizzas = _context.Pizza.ToList();
            var dto = _mapper.Map<List<PizzaDTO>>(pizzas);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PizzaDTO>> Create(PizzaDTO dto)
        {
            var entity = _mapper.Map<Pizza>(dto);
            _context.Pizza.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = entity.Id }, dto);
        }
    }
}
