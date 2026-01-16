using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Exercicio;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class ExercicioRepositorio : BaseRepositorio, IExercicioRepositorio
    {
        public ExercicioRepositorio(IConfiguration configurationcontexto) : base(configurationcontexto)
        {
        }
        public async Task<int> AdicionarExercicio(Exercicio exercicio)
        {
            return await _connection.QuerySingleAsync<int>(
               "spExercicioCriar",
                new
                {
                    exercicio.Nome,
                    exercicio.GrupoMuscular,
                    exercicio.Equipamento,
                    exercicio.Descricao

                },
                commandType: CommandType.StoredProcedure
                );
        }

        public async Task AtualizarExercicio(Exercicio exercicio)
        {
            await _connection.ExecuteAsync(
               "spExercicioAtualizar",
                new
                {
                    exercicio.ExercicioId,
                    exercicio.Nome,
                    exercicio.GrupoMuscular,
                    exercicio.Equipamento
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public async Task DeletarExercicio(int exercicioId)
        {
            await _connection.ExecuteAsync(
               "spExercicioDeletar",
                new
                {
                    ExercicioId = exercicioId
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public async Task<IEnumerable<Exercicio>> ListarPorGrupoMuscular(string grupoMuscular)
        {
            return await _connection.QueryAsync<Exercicio>(
               "spExercicioPorGrupoMuscular",
                new
                {
                    GrupoMuscular = grupoMuscular
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public async Task<Exercicio?> ObterPorID(int exercicioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<Exercicio>(
               "spExercicioObter",
                new
                {
                    ExercicioId = exercicioId
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public async Task<IEnumerable<Exercicio>> ObterTodosExercicios()
        {
            return await _connection.QueryAsync<Exercicio>(
                "spExercicioListar",
                 commandType: CommandType.StoredProcedure
                 );
        }

        public async Task<ExercicioResumoDto?> TotalTreinosPorExercicio(int exercicioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<ExercicioResumoDto>(
     "SELECT * FROM vwExercicioResumo WHERE ExercicioId = @ExercicioId",
     new { ExercicioId = exercicioId }
 );

        }
        public async Task<ExercicioDetalhadoDto?> ObterExercicioDetalhado(int exercicioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<ExercicioDetalhadoDto>(
                "SELECT * FROM vwExercicioDetalhado WHERE ExercicioId = @ExercicioId",
                new { ExercicioId = exercicioId }
            );
        }
    }
}