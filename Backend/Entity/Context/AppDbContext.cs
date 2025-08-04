using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Jugador> Jugador { get; set; }
        public DbSet<Carta> Carta { get; set; }
        public DbSet<JugadorCarta> JugadorCarta { get; set; }
    }
}