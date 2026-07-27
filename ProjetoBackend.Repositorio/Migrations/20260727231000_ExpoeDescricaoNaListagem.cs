using Microsoft.EntityFrameworkCore.Migrations;
using ProjetoBackend.Repositorio.Sql;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Faz "spExercicioListar" devolver Descricao, ImagemUrl e VideoUrl.
    ///
    /// A funcao veio do T-SQL original trazendo so id, nome, grupo e equipamento —
    /// entao GET /api/exercicio devolvia a descricao sempre vazia, e a tela de
    /// catalogo nao tinha como exibir a orientacao de execucao sem uma requisicao
    /// extra por exercicio.
    ///
    /// Precisa de DROP antes: o PostgreSQL nao aceita CREATE OR REPLACE quando o
    /// tipo de retorno da funcao muda.
    /// </summary>
    public partial class ExpoeDescricaoNaListagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""DROP FUNCTION IF EXISTS "spExercicioListar"();""");
            migrationBuilder.Sql(ScriptsSql.Ler("03_exercicio.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Volta a assinatura reduzida original.
            migrationBuilder.Sql("""
                DROP FUNCTION IF EXISTS "spExercicioListar"();

                CREATE FUNCTION "spExercicioListar"()
                RETURNS TABLE (
                    "ExercicioId"   INTEGER,
                    "Nome"          VARCHAR(150),
                    "GrupoMuscular" INTEGER,
                    "Equipamento"   VARCHAR(80)
                )
                LANGUAGE sql
                STABLE
                SET search_path = public
                AS $$
                    SELECT e."ExercicioId", e."Nome", e."GrupoMuscular", e."Equipamento"
                    FROM "Exercicios" e
                    ORDER BY e."GrupoMuscular", e."Nome";
                $$;
                """);
        }
    }
}
