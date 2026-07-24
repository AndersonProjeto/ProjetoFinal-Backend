using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Exercicio;
using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Dominio.Enum;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    /// <summary>
    /// No PostgreSQL os objetos sp* sao FUNCTIONS, chamadas com SQL de texto
    /// em vez de CommandType.StoredProcedure.
    /// </summary>
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
                """
                SELECT "spExercicioCriar"(@Nome, @GrupoMuscular, @Equipamento, @Descricao, @ImagemUrl, @VideoUrl)
                """,
                new
                {
                    exercicio.Nome,
                    // O enum vai como int: a coluna "GrupoMuscular" e integer.
                    GrupoMuscular = (int)exercicio.GrupoMuscular,
                    exercicio.Equipamento,
                    exercicio.Descricao,
                    exercicio.ImagemUrl,
                    exercicio.VideoUrl
                }
            );
        }

        public async Task AtualizarExercicio(Exercicio exercicio)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                """
                SELECT "spExercicioAtualizar"(@ExercicioId, @Nome, @GrupoMuscular, @Equipamento, @Descricao, @ImagemUrl, @VideoUrl)
                """,
                new
                {
                    exercicio.ExercicioId,
                    exercicio.Nome,
                    GrupoMuscular = (int)exercicio.GrupoMuscular,
                    exercicio.Equipamento,
                    exercicio.Descricao,
                    exercicio.ImagemUrl,
                    exercicio.VideoUrl
                }
            );
        }

        public async Task DeletarExercicio(int exercicioId)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                """
                SELECT "spExercicioDeletar"(@ExercicioId)
                """,
                new { ExercicioId = exercicioId }
            );
        }

        public async Task<IEnumerable<Exercicio>> ListarPorGrupoMuscular(EnumGrupoMuscular grupoMuscular)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<Exercicio>(
                """
                SELECT * FROM "spExercicioPorGrupoMuscular"(@GrupoMuscular)
                """,
                new { GrupoMuscular = (int)grupoMuscular }
            );
        }

        public async Task<Exercicio?> ObterPorID(int exercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Exercicio>(
                """
                SELECT * FROM "spExercicioObter"(@ExercicioId)
                """,
                new { ExercicioId = exercicioId }
            );
        }

        public async Task<IEnumerable<Exercicio>> ObterTodosExercicios()
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<Exercicio>(
                """
                SELECT * FROM "spExercicioListar"()
                """
            );
        }

        public async Task<ExercicioResumoDto?> TotalTreinosPorExercicio(int exercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<ExercicioResumoDto>(
                """
                SELECT * FROM "vwExercicioResumo" WHERE "ExercicioId" = @ExercicioId
                """,
                new { ExercicioId = exercicioId }
            );
        }

        public async Task<ExercicioDetalhadoDto?> ObterExercicioDetalhado(int exercicioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<ExercicioDetalhadoDto>(
                """
                SELECT * FROM "vwExercicioDetalhado" WHERE "ExercicioId" = @ExercicioId
                """,
                new { ExercicioId = exercicioId }
            );
        }

        public async Task<PaginaResultado<Exercicio>> ObterExerciciosPaginados(int pagina, int tamanhoPagina)
        {
            using var conn = CriarConexao();

            // A procedure original devolvia dois result sets num QueryMultipleAsync.
            // Function no Postgres devolve apenas um, entao a contagem e a pagina
            // viraram duas chamadas independentes.
            var total = await conn.ExecuteScalarAsync<int>(
                """
                SELECT "spExercicioContarTodos"()
                """
            );

            var itens = await conn.QueryAsync<Exercicio>(
                """
                SELECT * FROM "spExercicioPaginacao"(@Pagina, @TamanhoPagina)
                """,
                new { Pagina = pagina, TamanhoPagina = tamanhoPagina }
            );

            return new PaginaResultado<Exercicio>
            {
                TotalItems = total,
                Items = itens
            };
        }
    }
}
