using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Treino;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    /// <summary>
    /// No PostgreSQL os objetos sp* sao FUNCTIONS, chamadas com SQL de texto
    /// em vez de CommandType.StoredProcedure.
    /// </summary>
    public class TreinoRepositorio : BaseRepositorio, ITreinoRepositorio
    {
        public TreinoRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AdicionarTreino(Treino treino)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                """
                SELECT "spTreinoCriar"(@UsuarioId, @NomeTreino)
                """,
                new
                {
                    treino.UsuarioId,
                    treino.NomeTreino
                }
            );
        }

        public async Task AtualizarTreino(Treino treino)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                """
                SELECT "spTreinoAtualizar"(@TreinoId, @NomeTreino)
                """,
                new
                {
                    treino.TreinoId,
                    treino.NomeTreino
                }
            );
        }

        public async Task DeletarTreino(int treinoId)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                """
                SELECT "spTreinoDeletar"(@TreinoId)
                """,
                new { TreinoId = treinoId }
            );
        }

        public async Task<Treino?> ObterPorId(int treinoId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Treino>(
                """
                SELECT * FROM "spTreinoObterPorID"(@TreinoId)
                """,
                new { TreinoId = treinoId }
            );
        }

        public async Task<IEnumerable<TreinoPorUsuarioDTO>> ListarPorUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<TreinoPorUsuarioDTO>(
                """
                SELECT * FROM "vwTreinosPorUsuario" WHERE "UsuarioId" = @UsuarioId
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<TreinoResumoDTO>> ObterResumoTreinos(int usuarioId)
        {
            using var conn = CriarConexao();

            // Filtra na origem: sem o WHERE, a view devolve os treinos de todos os usuários.
            return await conn.QueryAsync<TreinoResumoDTO>(
                """
                SELECT * FROM "vwTreinoResumo" WHERE "UsuarioId" = @UsuarioId
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<int> TotalExerciciosDoTreino(int treinoId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<int>(
                """
                SELECT "fnTreinoTotalExercicios"(@TreinoId)
                """,
                new { TreinoId = treinoId }
            );
        }

        public async Task<int> TotalTreinosDoUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<int>(
                """
                SELECT "fnTreinoTotalUsuario"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<Treino>> ListarEntidadesPorUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<Treino>(
                """
                SELECT * FROM "spTreinoListarPorUsuario"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }
    }
}
