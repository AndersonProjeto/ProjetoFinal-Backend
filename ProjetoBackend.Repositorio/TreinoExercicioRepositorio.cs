using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
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
                "spTreinoExercicioCriar",
                new
                {
                    treinoExercicio.TreinoId,
                    treinoExercicio.ExercicioId,
                    treinoExercicio.Series,
                    treinoExercicio.Repeticoes,
                    treinoExercicio.DescansoSegundos
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task AtualizarTreinoExercicio(TreinoExercicio treinoExercicio)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                "spTreinoExercicioAtualizar",
                new
                {
                    treinoExercicio.TreinoExercicioId,
                    treinoExercicio.Series,
                    treinoExercicio.Repeticoes,
                    treinoExercicio.DescansoSegundos
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeletarTreinoExercicio(int treinoExercicioId)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                "spTreinoExercicioDeletar",
                new { TreinoExercicioId = treinoExercicioId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<TreinoExercicioDTO>> ListarTreino(int treinoId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<TreinoExercicioDTO>(
                "SELECT * FROM vwTreinoExerciciosDetalhe WHERE TreinoId = @TreinoId",
                new { TreinoId = treinoId }
            );
        }

        public async Task<TreinoExercicio?> ObterPorID(int TreinoExercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryFirstOrDefaultAsync<TreinoExercicio>(
                "spTreinoExercicioObter",
                new { TreinoExercicioId = TreinoExercicioId },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}