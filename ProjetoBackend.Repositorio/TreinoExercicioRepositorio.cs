using Dapper;
using ProjetoBackend.Aplicacao.DTOs.TreinoExercicio;
using ProjetoBackend.Dominio;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class TreinoExercicioRepositorio : BaseRepositorio, ITreinoExercicioRepositorio
    {
        public TreinoExercicioRepositorio(ProjetoContexto contexto)
            : base(contexto)
        {
        }

        public async Task<int> AdicionarTreinoExercicio(TreinoExercicio treinoExercicio)
        {
            return await _connection.QuerySingleAsync<int>(
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
            await _connection.ExecuteAsync(
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
            await _connection.ExecuteAsync(
                "spTreinoExercicioDeletar",
                new { TreinoExercicioId = treinoExercicioId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<TreinoExercicioDTO>> ListarTreino(int treinoId)
        {
            return await _connection.QueryAsync<TreinoExercicioDTO>(
                "SELECT * FROM vwTreinoExerciciosDetalhe WHERE TreinoId = @TreinoId",
                new { TreinoId = treinoId }
            );
        }
    }
}