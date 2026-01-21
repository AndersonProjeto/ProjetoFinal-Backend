using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Exercicio;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Dominio.Enum;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class ExercicioRepositorio : BaseRepositorio, IExercicioRepositorio
    {
        public ExercicioRepositorio(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AdicionarExercicio(Exercicio exercicio)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
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
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
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
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                "spExercicioDeletar",
                new { ExercicioId = exercicioId },
                commandType: CommandType.StoredProcedure
            );
        }

   
        public async Task<IEnumerable<Exercicio>> ListarPorGrupoMuscular(EnumGrupoMuscular grupoMuscular)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<Exercicio>(
                "spExercicioPorGrupoMuscular",
                new { GrupoMuscular = grupoMuscular },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Exercicio?> ObterPorID(int exercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Exercicio>(
                "spExercicioObter",
                new { ExercicioId = exercicioId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Exercicio>> ObterTodosExercicios()
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<Exercicio>(
                "spExercicioListar",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<ExercicioResumoDto?> TotalTreinosPorExercicio(int exercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<ExercicioResumoDto>(
                "SELECT * FROM vwExercicioResumo WHERE ExercicioId = @ExercicioId",
                new { ExercicioId = exercicioId }
            );
        }

        public async Task<ExercicioDetalhadoDto?> ObterExercicioDetalhado(int exercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<ExercicioDetalhadoDto>(
                "SELECT * FROM vwExercicioDetalhado WHERE ExercicioId = @ExercicioId",
                new { ExercicioId = exercicioId }
            );
        }

        public async Task<PaginaResultado<Exercicio>> ObterExerciciosPaginados(int pagina, int tamanhoPagina)
        {
            using var conn = CriarConexao();

            using var multi = await conn.QueryMultipleAsync(
                "spExercicioPaginacao",
                new { Pagina = pagina, TamanhoPagina = tamanhoPagina },
                commandType: CommandType.StoredProcedure
            );

            var total = await multi.ReadSingleAsync<int>();
            var itens = await multi.ReadAsync<Exercicio>();

            return new PaginaResultado<Exercicio>
            {
                TotalItems = total,
                Items = itens
            };
        }
    }
}