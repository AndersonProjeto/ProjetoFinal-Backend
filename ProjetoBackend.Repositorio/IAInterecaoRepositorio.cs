using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class IAInterecaoRepositorio : BaseRepositorio, IIAInteracaoRepositorio
    {
        public IAInterecaoRepositorio(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AdicionarIAInteracao(IAInteracao interacao)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                "spIAInteracaoCriar",
                new
                {
                    interacao.UsuarioId,
                    interacao.Pergunta,
                    interacao.Resposta
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<IAInteracao>> ListarIAInteracoesPorUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<IAInteracao>(
                "spIAInteracaoListarPorUsuario",
                new
                {
                    UsuarioId = usuarioId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IAInteracao?> ObterUltimaInteracao(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<IAInteracao>(
                "spIAInteracaoObterUltima",
                new
                {
                    UsuarioId = usuarioId
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}