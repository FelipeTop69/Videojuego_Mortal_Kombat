namespace Entity.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int RolId { get; set; }
        public Rol Rol { get; set; } = null!;
    }
}