using Business.JWTService.Interfaces;
using Entity.Context;
using Entity.DTOs.Auth;
using Microsoft.EntityFrameworkCore;

namespace Business.JWTService
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO loginRequest)
        {
            var user = await _context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username);

            // Validación de usuario y contraseña (sin hasheo)
            if (user == null || user.Password != loginRequest.Password)
                return null!;

            // Se pasa también el rol al generar el token
            var token = _jwtService.GenerateToken(user.Username, user.Rol.Nombre);

            return new LoginResponseDTO
            {
                Token = token
            };
        }
    }
}
