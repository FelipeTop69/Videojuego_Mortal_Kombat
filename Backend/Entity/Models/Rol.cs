﻿namespace Entity.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<Usuario> Usuarios { get; set; } = [];
    }
}
