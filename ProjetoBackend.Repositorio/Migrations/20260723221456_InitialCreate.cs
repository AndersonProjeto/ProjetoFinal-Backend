using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercicios",
                columns: table => new
                {
                    ExercicioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    GrupoMuscular = table.Column<int>(type: "integer", maxLength: 80, nullable: false),
                    Equipamento = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    ImagemUrl = table.Column<string>(type: "text", nullable: true),
                    VideoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicios", x => x.ExercicioId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AlturaCm = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'utc')"),
                    AvatarEstilo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AvatarSeed = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Evolucoes",
                columns: table => new
                {
                    EvolucaoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    PesoKg = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    CinturaCm = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    BracoCm = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    CoxaCm = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    DataRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolucoes", x => x.EvolucaoId);
                    table.ForeignKey(
                        name: "FK_Evolucoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IAInteracoes",
                columns: table => new
                {
                    IAInteracaoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Pergunta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Resposta = table.Column<string>(type: "text", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IAInteracoes", x => x.IAInteracaoId);
                    table.ForeignKey(
                        name: "FK_IAInteracoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IARelatorio",
                columns: table => new
                {
                    IARelatorioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Relatorio = table.Column<string>(type: "text", nullable: false),
                    DataGerado = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Treinos",
                columns: table => new
                {
                    TreinoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    NomeTreino = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinos", x => x.TreinoId);
                    table.ForeignKey(
                        name: "FK_Treinos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreinoExercicios",
                columns: table => new
                {
                    TreinoExercicioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TreinoId = table.Column<int>(type: "integer", nullable: false),
                    ExercicioId = table.Column<int>(type: "integer", nullable: false),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Repeticoes = table.Column<int>(type: "integer", nullable: false),
                    DescansoSegundos = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinoExercicios", x => x.TreinoExercicioId);
                    table.ForeignKey(
                        name: "FK_TreinoExercicios_Exercicios_ExercicioId",
                        column: x => x.ExercicioId,
                        principalTable: "Exercicios",
                        principalColumn: "ExercicioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreinoExercicios_Treinos_TreinoId",
                        column: x => x.TreinoId,
                        principalTable: "Treinos",
                        principalColumn: "TreinoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evolucoes_DataRegistro",
                table: "Evolucoes",
                column: "DataRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_Evolucoes_UsuarioId",
                table: "Evolucoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_IAInteracoes_UsuarioId",
                table: "IAInteracoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_IARelatorio_UsuarioId",
                table: "IARelatorio",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TreinoExercicios_ExercicioId",
                table: "TreinoExercicios",
                column: "ExercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_TreinoExercicios_TreinoId",
                table: "TreinoExercicios",
                column: "TreinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Treinos_UsuarioId",
                table: "Treinos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
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
