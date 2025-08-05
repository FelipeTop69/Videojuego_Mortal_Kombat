using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resistencia = table.Column<int>(type: "int", nullable: false),
                    Fuerza = table.Column<int>(type: "int", nullable: false),
                    Salud = table.Column<int>(type: "int", nullable: false),
                    Elemento = table.Column<int>(type: "int", nullable: false),
                    Destreza = table.Column<int>(type: "int", nullable: false),
                    GolpeFinal = table.Column<int>(type: "int", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jugador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdenTurno = table.Column<int>(type: "int", nullable: false),
                    CantidadCartasGanadas = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ronda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Habilidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ronda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JugadorCarta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    CartaId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    RondaJugado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JugadorCarta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JugadorCarta_Carta_CartaId",
                        column: x => x.CartaId,
                        principalTable: "Carta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JugadorCarta_Jugador_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JugadorCarta_CartaId",
                table: "JugadorCarta",
                column: "CartaId");

            migrationBuilder.CreateIndex(
                name: "IX_JugadorCarta_JugadorId",
                table: "JugadorCarta",
                column: "JugadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JugadorCarta");

            migrationBuilder.DropTable(
                name: "Ronda");

            migrationBuilder.DropTable(
                name: "Carta");

            migrationBuilder.DropTable(
                name: "Jugador");
        }
    }
}
