using Microsoft.EntityFrameworkCore.Migrations;
using ProjetoBackend.Repositorio.Sql;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Aplica as functions e views do modulo Evolucao. O script vem embarcado no
    /// assembly, entao o deploy nao depende de rodar SQL manualmente no Supabase.
    /// </summary>
    public partial class ObjetosSqlEvolucao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(ScriptsSql.Ler("02_evolucao.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Views primeiro: dependem das functions escalares.
            migrationBuilder.Sql("""
                DROP VIEW IF EXISTS "vwEvolucaoResumo";
                DROP VIEW IF EXISTS "vwEvolucaoHistorico";

                DROP FUNCTION IF EXISTS "spEvolucaoListarPorUsuario"(INTEGER);
                DROP FUNCTION IF EXISTS "spEvolucaoObterUltima"(INTEGER);
                DROP FUNCTION IF EXISTS "spEvolucaoObter"(INTEGER);
                DROP FUNCTION IF EXISTS "spEvolucaoAtualizar"(INTEGER, NUMERIC, NUMERIC, NUMERIC, NUMERIC);
                DROP FUNCTION IF EXISTS "spEvolucaoCriar"(INTEGER, NUMERIC, NUMERIC, NUMERIC, NUMERIC, TIMESTAMP);

                DROP FUNCTION IF EXISTS "fnEvolucaoConsultarIMC"(INTEGER);

                DROP FUNCTION IF EXISTS "fnEvolucaoDiferencaCoxa"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoDiferencaBraco"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoDiferencaCintura"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoDiferencaPeso"(INTEGER);

                DROP FUNCTION IF EXISTS "fnEvolucaoCoxaInicial"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoBracoInicial"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoCinturaInicial"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoPesoInicial"(INTEGER);

                DROP FUNCTION IF EXISTS "fnEvolucaoCoxaAtual"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoBracoAtual"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoCinturaAtual"(INTEGER);
                DROP FUNCTION IF EXISTS "fnEvolucaoPesoAtual"(INTEGER);
                """);
        }
    }
}
