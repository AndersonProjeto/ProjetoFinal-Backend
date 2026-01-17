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
            return await _connection.QuerySingleAsync<int>(
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
            await _connection.ExecuteAsync(
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
            await _connection.ExecuteAsync(
                "spTreinoDeletar",
                new { TreinoId = treinoId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Treino?> ObterPorId(int treinoId)
        {
            return await _connection.QuerySingleOrDefaultAsync<Treino>(
                "spTreinoObterPorID",
                new { TreinoId = treinoId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<TreinoPorUsuarioDTO>> ListarPorUsuario(int usuarioId)
        {
            return await _connection.QueryAsync<TreinoPorUsuarioDTO>(
    "SELECT * FROM vwTreinosPorUsuario WHERE UsuarioId = @UsuarioId",
    new { UsuarioId = usuarioId }
);

        }

        public async Task<IEnumerable<TreinoResumoDTO>> ObterResumoTreinos()
        {
            return await _connection.QueryAsync<TreinoResumoDTO>(
     "SELECT * FROM vwTreinoResumo"
 );
        }

        public async Task<int> TotalExerciciosDoTreino(int treinoId)
        {
            return await _connection.ExecuteScalarAsync<int>(
                "SELECT dbo.fnTreinoTotalExercicios(@TreinoId)",
                new { TreinoId = treinoId }
            );
        }

        public async Task<int> TotalTreinosDoUsuario(int usuarioId)
        {
            return await _connection.ExecuteScalarAsync<int>(
                "SELECT dbo.fnTreinoTotalUsuario(@UsuarioId)",
                new { UsuarioId = usuarioId }
            );
        }
    }
}