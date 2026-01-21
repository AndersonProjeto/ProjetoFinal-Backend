using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Treino;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class TreinoRepositorio : BaseRepositorio, ITreinoRepositorio
    {
        public TreinoRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AdicionarTreino(Treino treino)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                "spTreinoCriar",
                new
                {
                    treino.UsuarioId,
                    treino.NomeTreino
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task AtualizarTreino(Treino treino)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                "spTreinoAtualizar",
                new
                {
                    treino.TreinoId,
                    treino.NomeTreino
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeletarTreino(int treinoId)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                "spTreinoDeletar",
                new { TreinoId = treinoId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Treino?> ObterPorId(int treinoId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Treino>(
                "spTreinoObterPorID",
                new { TreinoId = treinoId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<TreinoPorUsuarioDTO>> ListarPorUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<TreinoPorUsuarioDTO>(
                "SELECT * FROM vwTreinosPorUsuario WHERE UsuarioId = @UsuarioId",
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<TreinoResumoDTO>> ObterResumoTreinos()
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<TreinoResumoDTO>(
                "SELECT * FROM vwTreinoResumo"
            );
        }

        public async Task<int> TotalExerciciosDoTreino(int treinoId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<int>(
                "SELECT dbo.fnTreinoTotalExercicios(@TreinoId)",
                new { TreinoId = treinoId }
            );
        }

        public async Task<int> TotalTreinosDoUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<int>(
                "SELECT dbo.fnTreinoTotalUsuario(@UsuarioId)",
                new { UsuarioId = usuarioId }
            );
        }
    }
}