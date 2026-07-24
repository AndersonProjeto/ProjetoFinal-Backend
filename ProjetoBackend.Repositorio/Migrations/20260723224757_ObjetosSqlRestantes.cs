using Microsoft.EntityFrameworkCore.Migrations;
using ProjetoBackend.Repositorio.Sql;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Aplica os objetos SQL dos modulos restantes (Usuario, Exercicio, Treino,
    /// TreinoExercicio, IAInteracao, IARelatorio) e reaplica o de Evolucao, que
    /// ganhou "SET search_path = public" nas funcoes depois da primeira versao.
    ///
    /// Todos os scripts usam CREATE OR REPLACE, entao reexecutar e seguro.
    /// </summary>
    public partial class ObjetosSqlRestantes : Migration
    {
        // Ordem importa: 01 cria as funcoes que a view de resumo de usuario usa,
        // e 03 cria a funcao que a view de resumo de exercicio usa.
        private static readonly string[] Scripts =
        {
            "01_usuario.sql",
            "02_evolucao.sql",
            "03_exercicio.sql",
            "04_treino.sql",
            "05_treinoexercicio.sql",
            "06_iainteracao.sql",
            "07_iarelatorio.sql"
        };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var script in Scripts)
                migrationBuilder.Sql(ScriptsSql.Ler(script));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Views antes das functions: as de resumo dependem das escalares.
            migrationBuilder.Sql("""
                DROP VIEW IF EXISTS "vwUsuarioResumo";
                DROP VIEW IF EXISTS "vwUsuarioUltimaEvolucao";
                DROP VIEW IF EXISTS "vwUsuarioDetalhes";
                DROP VIEW IF EXISTS "vwExercicioContagemGrupoMusucular";
                DROP VIEW IF EXISTS "vwExercicioDetalhado";
                DROP VIEW IF EXISTS "vwExercicioResumo";
                DROP VIEW IF EXISTS "vwTreinoResumo";
                DROP VIEW IF EXISTS "vwTreinosPorUsuario";
                DROP VIEW IF EXISTS "vwTreinoExerciciosDetalhe";
                DROP VIEW IF EXISTS "vwIAInteracoesUsuario";

                DROP FUNCTION IF EXISTS "spUsuarioObterPorEmail"(VARCHAR);
                DROP FUNCTION IF EXISTS "spUsuarioObter"(INTEGER);
                DROP FUNCTION IF EXISTS "spUsuarioDeletar"(INTEGER);
                DROP FUNCTION IF EXISTS "spUsuarioAtualizarSenha"(INTEGER, TEXT);
                DROP FUNCTION IF EXISTS "spUsuarioAtualizar"(INTEGER, VARCHAR, VARCHAR, TIMESTAMP, NUMERIC, VARCHAR, VARCHAR);
                DROP FUNCTION IF EXISTS "spUsuarioCriar"(VARCHAR, VARCHAR, TEXT, TIMESTAMP, NUMERIC, VARCHAR, VARCHAR);
                DROP FUNCTION IF EXISTS "fnCalcularIMC"(NUMERIC, NUMERIC);
                DROP FUNCTION IF EXISTS "fnCalcularIdade"(DATE);

                DROP FUNCTION IF EXISTS "spExercicioPaginacao"(INTEGER, INTEGER);
                DROP FUNCTION IF EXISTS "spExercicioContarTodos"();
                DROP FUNCTION IF EXISTS "spExercicioPorGrupoMuscular"(INTEGER);
                DROP FUNCTION IF EXISTS "spExercicioObter"(INTEGER);
                DROP FUNCTION IF EXISTS "spExercicioListar"();
                DROP FUNCTION IF EXISTS "spExercicioDeletar"(INTEGER);
                DROP FUNCTION IF EXISTS "spExercicioAtualizar"(INTEGER, VARCHAR, INTEGER, VARCHAR, TEXT, TEXT, TEXT);
                DROP FUNCTION IF EXISTS "spExercicioCriar"(VARCHAR, INTEGER, VARCHAR, TEXT, TEXT, TEXT);
                DROP FUNCTION IF EXISTS "fnExercicioTotalTreino"(INTEGER);

                DROP FUNCTION IF EXISTS "spTreinoListarPorUsuario"(INTEGER);
                DROP FUNCTION IF EXISTS "spTreinoObterPorID"(INTEGER);
                DROP FUNCTION IF EXISTS "spTreinoDeletar"(INTEGER);
                DROP FUNCTION IF EXISTS "spTreinoAtualizar"(INTEGER, VARCHAR);
                DROP FUNCTION IF EXISTS "spTreinoCriar"(INTEGER, VARCHAR);
                DROP FUNCTION IF EXISTS "fnTreinoTotalUsuario"(INTEGER);
                DROP FUNCTION IF EXISTS "fnTreinoTotalExercicios"(INTEGER);

                DROP FUNCTION IF EXISTS "spTreinoExercicioListarPorTreino"(INTEGER);
                DROP FUNCTION IF EXISTS "spTreinoExercicioObter"(INTEGER);
                DROP FUNCTION IF EXISTS "spTreinoExercicioDeletar"(INTEGER);
                DROP FUNCTION IF EXISTS "spTreinoExercicioAtualizar"(INTEGER, INTEGER, INTEGER, INTEGER);
                DROP FUNCTION IF EXISTS "spTreinoExercicioCriar"(INTEGER, INTEGER, INTEGER, INTEGER, INTEGER);

                DROP FUNCTION IF EXISTS "spIAInteracaoObterUltimos"(INTEGER, INTEGER);
                DROP FUNCTION IF EXISTS "spIAInteracaoObterUltima"(INTEGER);
                DROP FUNCTION IF EXISTS "spIAInteracaoListarPorUsuario"(INTEGER);
                DROP FUNCTION IF EXISTS "spIAInteracaoCriar"(INTEGER, TEXT, TEXT);

                DROP FUNCTION IF EXISTS "spIARelatorioListarPorUsuario"(INTEGER);
                DROP FUNCTION IF EXISTS "spIARelatorioObterUltimo"(INTEGER);
                DROP FUNCTION IF EXISTS "spIARelatorioCriar"(INTEGER, TEXT);
                """);
        }
    }
}
