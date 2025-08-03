using AutoMapper;
using Entity.Context;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RolController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RolDTO>> GetAll()
        {
            var roles = _context.Rol.ToList();
            var dto = _mapper.Map<List<RolDTO>>(roles);
            return Ok(dto);
        }
    }
}
