using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class Nome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "IARelatorio",
                columns: table => new
                {
                    IARelatorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Relatorio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataGerado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IARelatorio", x => x.IARelatorioId);
                    table.ForeignKey(
                        name: "FK_IARelatorio_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

         


            migrationBuilder.CreateIndex(
                name: "IX_IARelatorio_UsuarioId",
                table: "IARelatorio",
                column: "UsuarioId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evolucoes");

            migrationBuilder.DropTable(
                name: "IAInteracoes");

            migrationBuilder.DropTable(
                name: "IARelatorio");

            migrationBuilder.DropTable(
                name: "TreinoExercicios");

            migrationBuilder.DropTable(
                name: "Exercicios");

            migrationBuilder.DropTable(
                name: "Treinos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
