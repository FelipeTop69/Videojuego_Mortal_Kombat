using AutoMapper;
using Entity.Context;
using Entity.DTOs;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClienteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> GetAll()
        {
            var clientes = _context.Cliente.ToList();
            var dto = _mapper.Map<List<ClienteDTO>>(clientes);
            return Ok(dto);
        }
    }
}
