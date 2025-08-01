using Business.JWTService;
using Business.JWTService.Interfaces;
using Entity.Context;
using Entity.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(
            AppDbContext context, 
            AuthService authService, 
            IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var response = await _authService.AuthenticateAsync(loginRequest);
            if (response == null)
                return Unauthorized("Credenciales inválidas.");

            return Ok(response);
        }
    }
}