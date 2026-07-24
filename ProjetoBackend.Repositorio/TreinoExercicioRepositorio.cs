using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    /// <summary>
    /// No PostgreSQL os objetos sp* sao FUNCTIONS, chamadas com SQL de texto
    /// em vez de CommandType.StoredProcedure.
    /// </summary>
    public class TreinoExercicioRepositorio : BaseRepositorio, ITreinoExercicioRepositorio
    {
        public TreinoExercicioRepositorio(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AdicionarTreinoExercicio(TreinoExercicio treinoExercicio)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                """
                SELECT "spTreinoExercicioCriar"(@TreinoId, @ExercicioId, @Series, @Repeticoes, @DescansoSegundos)
                """,
                new
                {
                    treinoExercicio.TreinoId,
                    treinoExercicio.ExercicioId,
                    treinoExercicio.Series,
                    treinoExercicio.Repeticoes,
                    treinoExercicio.DescansoSegundos
                }
            );
        }

        public async Task AtualizarTreinoExercicio(TreinoExercicio treinoExercicio)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                """
                SELECT "spTreinoExercicioAtualizar"(@TreinoExercicioId, @Series, @Repeticoes, @DescansoSegundos)
                """,
                new
                {
                    treinoExercicio.TreinoExercicioId,
                    treinoExercicio.Series,
                    treinoExercicio.Repeticoes,
                    treinoExercicio.DescansoSegundos
                }
            );
        }

        public async Task DeletarTreinoExercicio(int treinoExercicioId)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                """
                SELECT "spTreinoExercicioDeletar"(@TreinoExercicioId)
                """,
                new { TreinoExercicioId = treinoExercicioId }
            );
        }

        public async Task<IEnumerable<TreinoExercicioDTO>> ListarTreino(int treinoId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<TreinoExercicioDTO>(
                """
                SELECT * FROM "vwTreinoExerciciosDetalhe" WHERE "TreinoId" = @TreinoId
                """,
                new { TreinoId = treinoId }
            );
        }

        public async Task<TreinoExercicio?> ObterPorID(int TreinoExercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryFirstOrDefaultAsync<TreinoExercicio>(
                """
                SELECT * FROM "spTreinoExercicioObter"(@TreinoExercicioId)
                """,
                new { TreinoExercicioId = TreinoExercicioId }
            );
        }
    }
}
